﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Tests.Infrastructure;
using NUnit.Framework;

namespace Azure.Messaging.ServiceBus.Tests.Receiver
{
    public class ProcessorLiveTests : ServiceBusLiveTestBase
    {
        [Test]
        [TestCase(1, false)]
        [TestCase(5, true)]
        [TestCase(10, false)]
        [TestCase(20, true)]
        public async Task ProcessEvent(int numThreads, bool autoComplete)
        {
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false))
            {
                await using var client = new ServiceBusClient(TestEnvironment.ServiceBusConnectionString);
                ServiceBusSender sender = client.CreateSender(scope.QueueName);

                // use double the number of threads so we can make sure we test that we don't
                // retrieve more messages than expected when there are more messages available
                using ServiceBusMessageBatch batch = await sender.CreateBatchAsync();
                var messageSendCt = numThreads * 2;
                ServiceBusMessageBatch messageBatch = AddMessages(batch, messageSendCt);

                await sender.SendAsync(messageBatch);

                var options = new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = numThreads,
                    AutoComplete = autoComplete,
                    MaxReceiveWaitTime = TimeSpan.FromSeconds(30)
                };
                var processor = client.CreateProcessor(scope.QueueName, options);
                int messageCt = 0;

                TaskCompletionSource<bool>[] completionSources = Enumerable
                .Range(0, numThreads)
                .Select(index => new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously))
                .ToArray();
                var completionSourceIndex = -1;

                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;
                await processor.StartProcessingAsync();

                async Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    try
                    {
                        var message = args.Message;
                        if (!autoComplete)
                        {
                            await args.CompleteAsync(message, args.CancellationToken);
                        }
                        Interlocked.Increment(ref messageCt);
                    }
                    finally
                    {

                        var setIndex = Interlocked.Increment(ref completionSourceIndex);
                        if (setIndex < numThreads)
                        {
                            completionSources[setIndex].SetResult(true);
                        }
                    }
                }
                await Task.WhenAll(completionSources.Select(source => source.Task));
                await processor.StopProcessingAsync();

                // we complete each task after one message being processed, so the total number of messages
                // processed should equal the number of threads, but it's possible that we may process a few more per thread.
                Assert.IsTrue(messageCt >= numThreads);
                Assert.IsTrue(messageCt < messageSendCt);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task UserSettlingWithAutoCompleteDoesNotThrow(int numThreads)
        {
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false))
            {
                await using var client = new ServiceBusClient(TestEnvironment.ServiceBusConnectionString);
                ServiceBusSender sender = client.CreateSender(scope.QueueName);

                // use double the number of threads so we can make sure we test that we don't
                // retrieve more messages than expected when there are more messages available
                using ServiceBusMessageBatch batch = await sender.CreateBatchAsync();
                var messageSendCt = numThreads * 2;
                ServiceBusMessageBatch messageBatch = AddMessages(batch, messageSendCt);

                await sender.SendAsync(messageBatch);

                var options = new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = numThreads,
                    AutoComplete = true,
                    MaxReceiveWaitTime = TimeSpan.FromSeconds(30)
                };
                var processor = client.CreateProcessor(scope.QueueName, options);
                int messageCt = 0;

                TaskCompletionSource<bool>[] completionSources = Enumerable
                .Range(0, numThreads)
                .Select(index => new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously))
                .ToArray();
                var completionSourceIndex = -1;

                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;
                await processor.StartProcessingAsync();

                async Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    try
                    {
                        var message = args.Message;
                        switch (numThreads)
                        {
                            case 1:
                                await args.CompleteAsync(message, args.CancellationToken);
                                break;
                            case 5:
                                await args.AbandonAsync(message);
                                break;
                            case 10:
                                await args.DeadLetterAsync(message);
                                break;
                            case 20:
                                await args.DeferAsync(message);
                                break;
                        }
                        Interlocked.Increment(ref messageCt);
                    }
                    finally
                    {

                        var setIndex = Interlocked.Increment(ref completionSourceIndex);
                        if (setIndex < numThreads)
                        {
                            completionSources[setIndex].SetResult(true);
                        }
                    }
                }
                await Task.WhenAll(completionSources.Select(source => source.Task));
                await processor.StopProcessingAsync();

                // we complete each task after one message being processed, so the total number of messages
                // processed should equal the number of threads, but it's possible that we may process a few more per thread.
                Assert.IsTrue(messageCt >= numThreads);
                Assert.IsTrue(messageCt < messageSendCt);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(20)]
        public async Task AutoLockRenewalWorks(int numThreads)
        {
            var lockDuration = TimeSpan.FromSeconds(10);
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false,
                lockDuration: lockDuration))
            {
                await using var client = GetClient();
                ServiceBusSender sender = client.CreateSender(scope.QueueName);

                using ServiceBusMessageBatch batch = await sender.CreateBatchAsync();
                var messageSendCt = numThreads;
                ServiceBusMessageBatch messageBatch = AddMessages(batch, messageSendCt);

                await sender.SendAsync(messageBatch);

                var options = new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = numThreads,
                    AutoComplete = false
                };
                var processor = client.CreateProcessor(scope.QueueName, options);
                int messageCt = 0;

                TaskCompletionSource<bool>[] completionSources = Enumerable
                .Range(0, numThreads)
                .Select(index => new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously))
                .ToArray();
                var completionSourceIndex = -1;

                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;
                await processor.StartProcessingAsync();

                async Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    try
                    {
                        var message = args.Message;
                        var lockedUntil = message.LockedUntil;
                        await Task.Delay(lockDuration);
                        Assert.That(message.LockedUntil > lockedUntil, $"{lockedUntil},{DateTime.UtcNow}");
                        await args.CompleteAsync(message, args.CancellationToken);
                        Interlocked.Increment(ref messageCt);
                    }
                    finally
                    {
                        var setIndex = Interlocked.Increment(ref completionSourceIndex);
                        if (setIndex < numThreads)
                        {
                            completionSources[setIndex].SetResult(true);
                        }
                    }
                }
                await Task.WhenAll(completionSources.Select(source => source.Task));
                await processor.StopProcessingAsync();
                Assert.AreEqual(numThreads, messageCt);
            }
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(5, 0)]
        [TestCase(10, 1)]
        [TestCase(20, 1)]
        public async Task MaxAutoLockRenewalDurationRespected(int numThreads, int autoLockRenewalDuration)
        {
            var lockDuration = TimeSpan.FromSeconds(10);
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false,
                lockDuration: lockDuration))
            {
                await using var client = GetClient();
                ServiceBusSender sender = client.CreateSender(scope.QueueName);

                using ServiceBusMessageBatch batch = await sender.CreateBatchAsync();
                var messageSendCt = numThreads;
                ServiceBusMessageBatch messageBatch = AddMessages(batch, messageSendCt);

                await sender.SendAsync(messageBatch);

                var options = new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = numThreads,
                    AutoComplete = false,
                    MaxAutoLockRenewalDuration = TimeSpan.FromSeconds(autoLockRenewalDuration)
                };
                var processor = client.CreateProcessor(scope.QueueName, options);
                int messageCt = 0;

                TaskCompletionSource<bool>[] completionSources = Enumerable
                .Range(0, numThreads)
                .Select(index => new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously))
                .ToArray();
                var completionSourceIndex = -1;

                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;
                await processor.StartProcessingAsync();

                async Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    try
                    {
                        var message = args.Message;
                        var lockedUntil = message.LockedUntil;
                        await Task.Delay(lockDuration.Add(TimeSpan.FromSeconds(1)));
                        if (!args.CancellationToken.IsCancellationRequested)
                        {
                            // only do the assertion if cancellation wasn't requested as otherwise
                            // the exception we would get is a TaskCanceledException rather than ServiceBusException
                            Assert.AreEqual(lockedUntil, message.LockedUntil);
                            Assert.That(
                                async () => await args.CompleteAsync(message, args.CancellationToken),
                                Throws.InstanceOf<ServiceBusException>().And.Property(nameof(ServiceBusException.Reason)).EqualTo(ServiceBusException.FailureReason.MessageLockLost));
                            Interlocked.Increment(ref messageCt);
                        }
                    }
                    finally
                    {
                        var setIndex = Interlocked.Increment(ref completionSourceIndex);
                        if (setIndex < numThreads)
                        {
                            completionSources[setIndex].SetResult(true);
                        }
                    }
                }
                await Task.WhenAll(completionSources.Select(source => source.Task));
                await processor.StopProcessingAsync();
                Assert.AreEqual(numThreads, messageCt);
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        public async Task CanStopProcessingFromHandler(int numThreads)
        {
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false))
            {
                await using var client = new ServiceBusClient(TestEnvironment.ServiceBusConnectionString);
                ServiceBusSender sender = client.CreateSender(scope.QueueName);
                int numMessages = 100;
                using ServiceBusMessageBatch batch = await sender.CreateBatchAsync();
                ServiceBusMessageBatch messageBatch = AddMessages(batch, numMessages);

                await sender.SendAsync(messageBatch);
                var options = new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = numThreads,
                    ReceiveMode = ReceiveMode.ReceiveAndDelete
                };
                var processor = client.CreateProcessor(scope.QueueName, options);
                int messageProcessedCt = 0;

                // stop processing halfway through
                int stopAfterMessagesCt = numMessages / 2;

                TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;

                await processor.StartProcessingAsync();

                Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    var currentCt = Interlocked.Increment(ref messageProcessedCt);
                    if (currentCt == stopAfterMessagesCt)
                    {
                        // awaiting here would cause a deadlock
                        _ = processor.StopProcessingAsync();
                        tcs.SetResult(true);
                    }
                    return Task.CompletedTask;
                }
                await tcs.Task;

                var receiver = GetNoRetryClient().CreateReceiver(scope.QueueName);
                var receivedMessages = await receiver.ReceiveBatchAsync(numMessages);
                // can't assert on the exact amount processed due to threads that
                // are already in flight when calling StopProcessingAsync, but we can at least verify that there are remaining messages
                Assert.IsTrue(receivedMessages.Count > 0);
                Assert.IsTrue(messageProcessedCt < numMessages);
            }
        }

        [Test]
        public async Task OnMessageExceptionHandlerCalled()
        {
            var invalidQueueName = "nonexistentqueuename";
            var exceptionReceivedHandlerCalled = false;
            var client = new ServiceBusClient(TestEnvironment.ServiceBusConnectionString);
            ServiceBusProcessor processor = client.CreateProcessor(invalidQueueName);

            processor.ProcessMessageAsync += ProcessMessage;
            processor.ProcessErrorAsync += ProcessErrors;

            Task ProcessMessage(ProcessMessageEventArgs args)
            {
                Assert.Fail("Unexpected exception: Did not expect messages here");
                return Task.CompletedTask;
            }

            Task ProcessErrors(ProcessErrorEventArgs args)
            {
                Assert.NotNull(args);
                Assert.NotNull(args.Exception);
                Assert.AreEqual(processor.FullyQualifiedNamespace, args.FullyQualifiedNamespace);
                Assert.AreEqual(ServiceBusErrorSource.Receive, args.ErrorSource);
                Assert.AreEqual(processor.EntityPath, args.EntityPath);

                if (args.Exception is ServiceBusException sbException)
                {
                    if (sbException.Reason == ServiceBusException.FailureReason.MessagingEntityNotFound ||
                        // There is a race condition wherein the service closes the connection when getting
                        // the request for the non-existant queue. If the connection is closed by the time
                        // our exception handling kicks in, we throw it as a ServiceCommunicationProblem
                        // as we cannot be sure the error wasn't due to the connection being closed,
                        // as opposed to what we know is the true cause in this case,
                        // MessagingEntityNotFound.
                        sbException.Reason == ServiceBusException.FailureReason.ServiceCommunicationProblem)
                    {
                        exceptionReceivedHandlerCalled = true;
                        return Task.CompletedTask;
                    }
                }

                Assert.Fail($"Unexpected exception: {args.Exception}");
                return Task.CompletedTask;
            }

            try
            {
                await processor.StartProcessingAsync();
                var stopwatch = Stopwatch.StartNew();
                while (stopwatch.Elapsed.TotalSeconds <= 20)
                {
                    if (exceptionReceivedHandlerCalled)
                    {
                        break;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                Assert.True(exceptionReceivedHandlerCalled);
            }
            finally
            {
                await processor.StopProcessingAsync();
            }
        }

        [Test]
        public async Task CannotAddHandlerWhileProcessorIsRunning()
        {
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false))
            {
                await using var client = GetClient();

                var processor = client.CreateProcessor(scope.QueueName);

                Func<ProcessMessageEventArgs, Task> eventHandler = eventArgs => Task.CompletedTask;
                Func<ProcessErrorEventArgs, Task> errorHandler = eventArgs => Task.CompletedTask;
                processor.ProcessMessageAsync += eventHandler;
                processor.ProcessErrorAsync += errorHandler;

                await processor.StartProcessingAsync();

                Assert.That(() => processor.ProcessMessageAsync -= eventHandler, Throws.InstanceOf<InvalidOperationException>());
                Assert.That(() => processor.ProcessErrorAsync -= errorHandler, Throws.InstanceOf<InvalidOperationException>());

                await processor.StopProcessingAsync();

                // Once stopped, the processor should allow handlers to be removed, and re-added.
                Assert.That(() => processor.ProcessMessageAsync -= eventHandler, Throws.Nothing);
                Assert.That(() => processor.ProcessErrorAsync -= errorHandler, Throws.Nothing);

                Assert.That(() => processor.ProcessMessageAsync += eventHandler, Throws.Nothing);
                Assert.That(() => processor.ProcessErrorAsync += errorHandler, Throws.Nothing);
            }
        }

        [Test]
        public async Task StopProcessingDoesNotCancelAutoCompletion()
        {
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false))
            {
                await using var client = GetClient();
                var sender = client.CreateSender(scope.QueueName);
                await sender.SendAsync(GetMessage());
                var processor = client.CreateProcessor(scope.QueueName, new ServiceBusProcessorOptions
                {
                    AutoComplete = true
                });
                var tcs = new TaskCompletionSource<bool>();

                Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    tcs.SetResult(true);
                    return Task.CompletedTask;
                }
                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;

                await processor.StartProcessingAsync();
                await tcs.Task;
                await processor.StopProcessingAsync();
                var receiver = client.CreateReceiver(scope.QueueName);
                var msg = await receiver.ReceiveAsync();
                Assert.IsNull(msg);
            }
        }

        [Test]
        [TestCase("")]
        [TestCase("Abandon")]
        [TestCase("Complete")]
        [TestCase("Defer")]
        [TestCase("Deadletter")]
        [TestCase("DeadletterOverload")]
        public async Task UserCallbackThrowingCausesMessageToBeAbandonedIfNotSettled(string settleMethod)
        {
            await using (var scope = await ServiceBusScope.CreateWithQueue(
                enablePartitioning: false,
                enableSession: false))
            {
                await using var client = GetClient();
                var sender = client.CreateSender(scope.QueueName);
                await sender.SendAsync(GetMessage());
                var processor = client.CreateProcessor(scope.QueueName, new ServiceBusProcessorOptions
                {
                    AutoComplete = true
                });
                var tcs = new TaskCompletionSource<bool>();

                async Task ProcessMessage(ProcessMessageEventArgs args)
                {
                    switch (settleMethod)
                    {
                        case "Abandon":
                            await args.AbandonAsync(args.Message);
                            break;
                        case "Complete":
                            await args.CompleteAsync(args.Message);
                            break;
                        case "Defer":
                            await args.DeferAsync(args.Message);
                            break;
                        case "Deadletter":
                            await args.DeadLetterAsync(args.Message);
                            break;
                        case "DeadletterOverload":
                            await args.DeadLetterAsync(args.Message, "reason");
                            break;
                    }
                    throw new TestException();
                }

                Task ExceptionHandler(ProcessErrorEventArgs args)
                {
                    tcs.SetResult(true);
                    if (!(args.Exception is TestException))
                    {
                        Assert.Fail(args.Exception.ToString());
                    }
                    return Task.CompletedTask;
                }
                processor.ProcessMessageAsync += ProcessMessage;
                processor.ProcessErrorAsync += ExceptionHandler;

                await processor.StartProcessingAsync();
                await tcs.Task;
                await processor.StopProcessingAsync();
                var receiver = client.CreateReceiver(scope.QueueName);
                var msg = await receiver.ReceiveAsync(TimeSpan.FromSeconds(5));
                if (settleMethod == "" || settleMethod == "Abandon")
                {
                    Assert.IsNotNull(msg);
                }
                else
                {
                    Assert.IsNull(msg);
                }
            }
        }
    }
}
