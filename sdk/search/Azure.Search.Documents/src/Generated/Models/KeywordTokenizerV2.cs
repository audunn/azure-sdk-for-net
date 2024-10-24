// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.Search.Documents.Models
{
    /// <summary> Emits the entire input as a single token. This tokenizer is implemented using Apache Lucene. </summary>
    public partial class KeywordTokenizerV2 : LexicalTokenizer
    {
        /// <summary> Initializes a new instance of KeywordTokenizerV2. </summary>
        /// <param name="name"> The name of the tokenizer. It must only contain letters, digits, spaces, dashes or underscores, can only start and end with alphanumeric characters, and is limited to 128 characters. </param>
        public KeywordTokenizerV2(string name) : base(name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            ODataType = "#Microsoft.Azure.Search.KeywordTokenizerV2";
        }

        /// <summary> Initializes a new instance of KeywordTokenizerV2. </summary>
        /// <param name="oDataType"> Identifies the concrete type of the tokenizer. </param>
        /// <param name="name"> The name of the tokenizer. It must only contain letters, digits, spaces, dashes or underscores, can only start and end with alphanumeric characters, and is limited to 128 characters. </param>
        /// <param name="maxTokenLength"> The maximum token length. Default is 256. Tokens longer than the maximum length are split. The maximum token length that can be used is 300 characters. </param>
        internal KeywordTokenizerV2(string oDataType, string name, int? maxTokenLength) : base(oDataType, name)
        {
            MaxTokenLength = maxTokenLength;
            ODataType = oDataType ?? "#Microsoft.Azure.Search.KeywordTokenizerV2";
        }

        /// <summary> The maximum token length. Default is 256. Tokens longer than the maximum length are split. The maximum token length that can be used is 300 characters. </summary>
        public int? MaxTokenLength { get; set; }
    }
}
