// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.Data.Tables.Models;

namespace Azure.Data.Tables
{
    /// <summary> The Service service client. </summary>
    internal partial class ServiceClient
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly HttpPipeline _pipeline;
        internal ServiceRestClient RestClient { get; }
        /// <summary> Initializes a new instance of ServiceClient for mocking. </summary>
        protected ServiceClient()
        {
        }
        /// <summary> Initializes a new instance of ServiceClient. </summary>
        internal ServiceClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string version = "2019-02-02")
        {
            RestClient = new ServiceRestClient(clientDiagnostics, pipeline, url, version);
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        /// <summary> Sets properties for a storage account&apos;s Table service endpoint, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="storageServiceProperties"> The StorageService properties. </param>
        /// <param name="timeout"> The The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/setting-timeouts-for-queue-service-operations&gt;Setting Timeouts for Queue Service Operations.&lt;/a&gt;. </param>
        /// <param name="requestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response> SetPropertiesAsync(StorageServiceProperties storageServiceProperties, int? timeout = null, string requestId = null, CancellationToken cancellationToken = default)
        {
            return (await RestClient.SetPropertiesAsync(storageServiceProperties, timeout, requestId, cancellationToken).ConfigureAwait(false)).GetRawResponse();
        }

        /// <summary> Sets properties for a storage account&apos;s Table service endpoint, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="storageServiceProperties"> The StorageService properties. </param>
        /// <param name="timeout"> The The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/setting-timeouts-for-queue-service-operations&gt;Setting Timeouts for Queue Service Operations.&lt;/a&gt;. </param>
        /// <param name="requestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response SetProperties(StorageServiceProperties storageServiceProperties, int? timeout = null, string requestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.SetProperties(storageServiceProperties, timeout, requestId, cancellationToken).GetRawResponse();
        }

        /// <summary> gets the properties of a storage account&apos;s Table service, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="timeout"> The The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/setting-timeouts-for-queue-service-operations&gt;Setting Timeouts for Queue Service Operations.&lt;/a&gt;. </param>
        /// <param name="requestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<StorageServiceProperties>> GetPropertiesAsync(int? timeout = null, string requestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.GetPropertiesAsync(timeout, requestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> gets the properties of a storage account&apos;s Table service, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="timeout"> The The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/setting-timeouts-for-queue-service-operations&gt;Setting Timeouts for Queue Service Operations.&lt;/a&gt;. </param>
        /// <param name="requestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<StorageServiceProperties> GetProperties(int? timeout = null, string requestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.GetProperties(timeout, requestId, cancellationToken);
        }

        /// <summary> Retrieves statistics related to replication for the Table service. It is only available on the secondary location endpoint when read-access geo-redundant replication is enabled for the storage account. </summary>
        /// <param name="timeout"> The The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/setting-timeouts-for-queue-service-operations&gt;Setting Timeouts for Queue Service Operations.&lt;/a&gt;. </param>
        /// <param name="requestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<StorageServiceStats>> GetStatisticsAsync(int? timeout = null, string requestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.GetStatisticsAsync(timeout, requestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Retrieves statistics related to replication for the Table service. It is only available on the secondary location endpoint when read-access geo-redundant replication is enabled for the storage account. </summary>
        /// <param name="timeout"> The The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/setting-timeouts-for-queue-service-operations&gt;Setting Timeouts for Queue Service Operations.&lt;/a&gt;. </param>
        /// <param name="requestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<StorageServiceStats> GetStatistics(int? timeout = null, string requestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.GetStatistics(timeout, requestId, cancellationToken);
        }
    }
}
