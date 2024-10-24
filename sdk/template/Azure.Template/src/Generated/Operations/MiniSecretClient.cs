// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.Template.Models;

namespace Azure.Template
{
    /// <summary> The MiniSecret service client. </summary>
    public partial class MiniSecretClient
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly HttpPipeline _pipeline;
        internal MiniSecretRestClient RestClient { get; }
        /// <summary> Initializes a new instance of MiniSecretClient for mocking. </summary>
        protected MiniSecretClient()
        {
        }
        /// <summary> Initializes a new instance of MiniSecretClient. </summary>
        internal MiniSecretClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string vaultBaseUrl, string apiVersion = "7.0")
        {
            RestClient = new MiniSecretRestClient(clientDiagnostics, pipeline, vaultBaseUrl, apiVersion);
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        /// <summary> The GET operation is applicable to any secret stored in Azure Key Vault. This operation requires the secrets/get permission. </summary>
        /// <param name="secretName"> The name of the secret. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<SecretBundle>> GetSecretAsync(string secretName, CancellationToken cancellationToken = default)
        {
            return await RestClient.GetSecretAsync(secretName, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> The GET operation is applicable to any secret stored in Azure Key Vault. This operation requires the secrets/get permission. </summary>
        /// <param name="secretName"> The name of the secret. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<SecretBundle> GetSecret(string secretName, CancellationToken cancellationToken = default)
        {
            return RestClient.GetSecret(secretName, cancellationToken);
        }
    }
}
