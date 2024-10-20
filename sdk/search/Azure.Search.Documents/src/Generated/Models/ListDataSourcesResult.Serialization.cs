// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.Search.Documents.Models
{
    internal partial class ListDataSourcesResult
    {
        internal static ListDataSourcesResult DeserializeListDataSourcesResult(JsonElement element)
        {
            IReadOnlyList<SearchIndexerDataSource> value = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("value"))
                {
                    List<SearchIndexerDataSource> array = new List<SearchIndexerDataSource>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.Null)
                        {
                            array.Add(null);
                        }
                        else
                        {
                            array.Add(SearchIndexerDataSource.DeserializeSearchIndexerDataSource(item));
                        }
                    }
                    value = array;
                    continue;
                }
            }
            return new ListDataSourcesResult(value);
        }
    }
}
