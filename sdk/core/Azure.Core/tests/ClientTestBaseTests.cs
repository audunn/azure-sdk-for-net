﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.TestFramework;
using NUnit.Framework;

namespace Azure.Core.Tests
{
    public class ClientTestBaseTests : ClientTestBase
    {
        public ClientTestBaseTests(bool isAsync) : base(isAsync)
        {
            TestDiagnostics = false;
        }

        [Test]
        public void AllowsUsingSyncMethodsWithoutAsyncAlternative()
        {
            TestClient client = InstrumentClient(new TestClient());
            var result = client.Method2();

            Assert.AreEqual("Hello", result);
        }

        [Test]
        public async Task CallsCorrectMethodBasedOnCtorArgument()
        {
            TestClient client = InstrumentClient(new TestClient());
            var result = await client.MethodAsync(123);

            Assert.AreEqual(IsAsync ? "Async 123 False" : "Sync 123 False", result);
        }

        [Test]
        public async Task WorksWithCancellationToken()
        {
            TestClient client = InstrumentClient(new TestClient());
            var result = await client.MethodAsync(123, new CancellationTokenSource().Token);

            Assert.AreEqual(IsAsync ? "Async 123 True" : "Sync 123 True", result);
        }

        [Test]
        public void ThrowsForInvalidClientTypes()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => InstrumentClient(new InvalidTestClient()));
            Assert.AreEqual("Client type contains public non-virtual async method MethodAsync", exception.Message);
        }

        [Test]
        public void ThrowsForSyncCallsInAsyncContext()
        {
            if (IsAsync)
            {
                TestClient client = InstrumentClient(new TestClient());
                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => client.Method(123));
                Assert.AreEqual("Async method call expected for Method", exception.Message);
            }
        }

        [Test]
        public void ThrowsWhenSyncMethodIsNotAvailable()
        {
            if (!IsAsync)
            {
                TestClient client = InstrumentClient(new TestClient());
                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => client.NoAlternativeAsync(123));
                Assert.AreEqual("Unable to find a method with name NoAlternative and System.Int32,System.Threading.CancellationToken parameters." +
                                " Make sure both methods have the same signature including the cancellationToken parameter", exception.Message);
            }
        }

        /// <summary>
        /// Validate the interceptor is ignored when we're using SyncOnly.
        /// </summary>
        [Test]
        [SyncOnly]
        public void SyncOnlyDoesNotIntercept()
        {
            TestClient client = InstrumentClient(new TestClient());
            client.Method(42);
        }

        /// <summary>
        /// Ensure we can resolve sync/async methods that only vary based on
        /// generic type parameters.
        /// </summary>
        [Test]
        public async Task CustomUserSchemaPatternResolves()
        {
            TestClient client = InstrumentClient(new TestClient());
            string responseDataPrefix = IsAsync ? "async" : "sync";

            // Static schema
            Response<string> staticData = await client.GetDataAsync<string>();
            Assert.AreEqual($"{responseDataPrefix} - static", staticData.GetRawResponse().ReasonPhrase);

            // Dynamic schema
            Response<object> dynamicData = await client.GetDataAsync();
            Assert.AreEqual($"{responseDataPrefix} - dynamic", dynamicData.GetRawResponse().ReasonPhrase);
        }

        /// <summary>
        /// Ensure failures in sync/async methods that only vary based on
        /// generic type parameters are thrown correctly.
        /// </summary>
        [Test]
        public async Task CustomUserSchemaPatternThrows()
        {
            TestClient client = InstrumentClient(new TestClient());
            string exceptionPrefix = IsAsync ? "async" : "sync";

            // Static schema
            try { await client.GetFailureAsync<string>(); }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual($"{exceptionPrefix} - static", ex.Message);
            }

            // Dynamic schema
            try { await client.GetFailureAsync(); }
            catch (InvalidOperationException ex)
            {
                Assert.AreEqual($"{exceptionPrefix} - dynamic", ex.Message);
            }
        }

        public class TestClient
        {
            public virtual Task<string> MethodAsync(int i, CancellationToken cancellationToken = default)
            {
                return Task.FromResult("Async " + i + " " + cancellationToken.CanBeCanceled);
            }

            public virtual Task<string> NoAlternativeAsync(int i, CancellationToken cancellationToken = default)
            {
                return Task.FromResult("I don't have sync alternative");
            }

            public virtual string Method(int i, CancellationToken cancellationToken = default)
            {
                return "Sync " + i + " " + cancellationToken.CanBeCanceled;
            }

            public virtual string Method2()
            {
                return "Hello";
            }

            // These four follow the new pattern for custom users schemas
            public virtual Task<Response<T>> GetDataAsync<T>() =>
                Task.FromResult(Response.FromValue(default(T), new MockResponse(200, "async - static")));
            public virtual Response<T> GetData<T>() =>
                Response.FromValue(default(T), new MockResponse(200, "sync - static"));
            public virtual Task<Response<object>> GetDataAsync() =>
                Task.FromResult(Response.FromValue((object)null, new MockResponse(200, "async - dynamic")));
            public virtual Response<object> GetData() =>
                Response.FromValue((object)null, new MockResponse(200, "sync - dynamic"));

            // These four follow the new pattern for custom users schemas and
            // throw exceptions
            public virtual Task<Response<T>> GetFailureAsync<T>() =>
                throw new InvalidOperationException("async - static");
            public virtual Response<T> GetFailure<T>() =>
                throw new InvalidOperationException("sync - static");
            public virtual Task<Response<object>> GetFailureAsync() =>
                throw new InvalidOperationException("async - dynamic");
            public virtual Response<object> GetFailure() =>
                throw new InvalidOperationException("sync - dynamic");
        }

        public class InvalidTestClient
        {
            public Task<string> MethodAsync(int i)
            {
                return Task.FromResult("Async " + i);
            }

            public virtual string Method(int i)
            {
                return "Sync " + i;
            }
        }
    }
}
