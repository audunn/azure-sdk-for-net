// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;
using Azure.Search.Documents.Models;

namespace Azure.Search.Documents
{
    /// <summary> The Documents service client. </summary>
    internal partial class DocumentsClient
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly HttpPipeline _pipeline;
        internal DocumentsRestClient RestClient { get; }
        /// <summary> Initializes a new instance of DocumentsClient for mocking. </summary>
        protected DocumentsClient()
        {
        }
        /// <summary> Initializes a new instance of DocumentsClient. </summary>
        internal DocumentsClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string endpoint, string indexName, string apiVersion = "2019-05-06-Preview")
        {
            RestClient = new DocumentsRestClient(clientDiagnostics, pipeline, endpoint, indexName, apiVersion);
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        /// <summary> Queries the number of documents in the index. </summary>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<long>> CountAsync(Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.CountAsync(xMsClientRequestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Queries the number of documents in the index. </summary>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<long> Count(Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.Count(xMsClientRequestId, cancellationToken);
        }

        /// <summary> Searches for documents in the index. </summary>
        /// <param name="searchRequest"> The definition of the Search request. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<SearchDocumentsResult>> SearchPostAsync(SearchOptions searchRequest, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.SearchPostAsync(searchRequest, xMsClientRequestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Searches for documents in the index. </summary>
        /// <param name="searchRequest"> The definition of the Search request. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<SearchDocumentsResult> SearchPost(SearchOptions searchRequest, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.SearchPost(searchRequest, xMsClientRequestId, cancellationToken);
        }

        /// <summary> Retrieves a document from the index. </summary>
        /// <param name="key"> The key of the document to retrieve. </param>
        /// <param name="selectedFields"> List of field names to retrieve for the document; Any field not retrieved will be missing from the returned document. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<IReadOnlyDictionary<string, object>>> GetAsync(string key, IEnumerable<string> selectedFields = null, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.GetAsync(key, selectedFields, xMsClientRequestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Retrieves a document from the index. </summary>
        /// <param name="key"> The key of the document to retrieve. </param>
        /// <param name="selectedFields"> List of field names to retrieve for the document; Any field not retrieved will be missing from the returned document. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<IReadOnlyDictionary<string, object>> Get(string key, IEnumerable<string> selectedFields = null, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.Get(key, selectedFields, xMsClientRequestId, cancellationToken);
        }

        /// <summary> Suggests documents in the index that match the given partial query text. </summary>
        /// <param name="suggestRequest"> The Suggest request. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<SuggestDocumentsResult>> SuggestPostAsync(SuggestOptions suggestRequest, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.SuggestPostAsync(suggestRequest, xMsClientRequestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Suggests documents in the index that match the given partial query text. </summary>
        /// <param name="suggestRequest"> The Suggest request. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<SuggestDocumentsResult> SuggestPost(SuggestOptions suggestRequest, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.SuggestPost(suggestRequest, xMsClientRequestId, cancellationToken);
        }

        /// <summary> Sends a batch of document write actions to the index. </summary>
        /// <param name="batch"> The batch of index actions. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<IndexDocumentsResult>> IndexAsync(IndexBatch batch, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.IndexAsync(batch, xMsClientRequestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Sends a batch of document write actions to the index. </summary>
        /// <param name="batch"> The batch of index actions. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<IndexDocumentsResult> Index(IndexBatch batch, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.Index(batch, xMsClientRequestId, cancellationToken);
        }

        /// <summary> Autocompletes incomplete query terms based on input text and matching terms in the index. </summary>
        /// <param name="autocompleteRequest"> The definition of the Autocomplete request. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<AutocompleteResults>> AutocompletePostAsync(AutocompleteOptions autocompleteRequest, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.AutocompletePostAsync(autocompleteRequest, xMsClientRequestId, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Autocompletes incomplete query terms based on input text and matching terms in the index. </summary>
        /// <param name="autocompleteRequest"> The definition of the Autocomplete request. </param>
        /// <param name="xMsClientRequestId"> The tracking ID sent with the request to help with debugging. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<AutocompleteResults> AutocompletePost(AutocompleteOptions autocompleteRequest, Guid? xMsClientRequestId = null, CancellationToken cancellationToken = default)
        {
            return RestClient.AutocompletePost(autocompleteRequest, xMsClientRequestId, cancellationToken);
        }
    }
}
