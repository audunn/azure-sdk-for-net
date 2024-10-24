// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.Search.Documents.Models
{
    /// <summary>
    /// <para>
    /// Represents a field in an index definition, which describes the name, data type, and search behavior of a field.
    /// </para>
    /// <para>
    /// When creating an index, instead use the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
    /// </para>
    /// </summary>
    [CodeGenModel("Field")]
    public partial class SearchField
    {
        // TODO: Replace constructor and read-only properties when https://github.com/Azure/autorest.csharp/issues/554 is fixed.

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchField"/> class.
        /// </summary>
        /// <param name="name">The name of the field, which must be unique within the index or parent field.</param>
        /// <param name="type">The data type of the field.</param>
        /// <exception cref="ArgumentException"><paramref name="name"/> is an empty string.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        public SearchField(string name, SearchFieldDataType type)
        {
            Argument.AssertNotNullOrEmpty(name, nameof(name));

            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        [CodeGenMember("name")]
        public string Name { get; }

        /// <summary>
        /// Ge the data type of the field.
        /// </summary>
        [CodeGenMember("type")]
        public SearchFieldDataType Type { get; }

        // TODO: Remove "overrides" for boolean properties when https://github.com/Azure/autorest.csharp/issues/558 is fixed.

        /// <summary>
        /// Gets or sets a value indicating whether the field is full-text searchable. The default is null.
        /// This means it will undergo analysis such as word-breaking during indexing.
        /// This property can be true only for <see cref="SearchFieldDataType.String"/> or "Collection(DataType.String)". It must be false for non-string simple fields, and null for complex fields.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Full-text searches enable the field value "sunny day" to be split into individual terms "sunny" and "day". This will increase the size of your index.
        /// </para>
        /// <para>
        /// This field must be set according to constraints described in the summary, or the server may respond with an error.
        /// Instead, consider using the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
        /// </para>
        /// </remarks>
        [CodeGenMember("searchable")]
        public bool? IsSearchable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field can be referenced in <c>$filter</c> queries. The default is null.
        /// This property must be null for complex fields, but can be set on simple fields within a complex field.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Filterable differs from searchable in how strings are handled. Fields of type <see cref="SearchFieldDataType.String"/> or "Collection(DataType.String)" that are filterable do not undergo word-breaking, so comparisons are for exact matches only.
        /// For example, if you set such a field <c>f</c> to "sunny day", <c>$filter=f eq 'sunny'</c> will find no matches, but <c>$filter=f eq 'sunny day'</c> will.
        /// </para>
        /// <para>
        /// This field must be set according to constraints described in the summary, or the server may respond with an error.
        /// Instead, consider using the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
        /// </para>
        /// </remarks>
        [CodeGenMember("filterable")]
        public bool? IsFilterable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field will be returned in a search result. The default is null.
        /// This property must be true for key fields, and must be null for complex fields.
        /// </summary>
        /// <remarks>
        /// <para>
        /// You can hide a field from search results if you want to use it only as a filter, for sorting, or for scoring.
        /// This property can also be changed on existing fields and enabling it does not cause an increase in index storage requirements.
        /// </para>
        /// <para>
        /// This field must be set according to constraints described in the summary, or the server may respond with an error.
        /// Instead, consider using the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
        /// </para>
        /// </remarks>
        public bool? IsHidden
        {
            get => IsRetrievable.HasValue ? !IsRetrievable : null;
            set => IsRetrievable = value.HasValue ? !value : null;
        }

        [CodeGenMember("retrievable")]
        private bool? IsRetrievable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field can be referenced in a <c>$orderby</c> expression. The default is null.
        /// A simple field can be sortable only if it is a single-valued type such as <see cref="SearchFieldDataType.String"/> or <see cref="SearchFieldDataType.Int32"/>.
        /// </summary>
        /// <remarks>
        /// This field must be set according to constraints described in the summary, or the server may respond with an error.
        /// Instead, consider using the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
        /// </remarks>
        [CodeGenMember("sortable")]
        public bool? IsSortable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the field can be retrieved in facet queries. The default is null.
        /// This property must be null for complex fields, but can be set on simple fields within a complex field.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Facets are used in presentation of search results that include hit counts by categories.
        /// For example, in a search for digital cameras, facets might include branch, megapixels, price, etc.
        /// </para>
        /// <para>
        /// This field must be set according to constraints described in the summary, or the server may respond with an error.
        /// Instead, consider using the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
        /// </para>
        /// </remarks>
        [CodeGenMember("facetable")]
        public bool? IsFacetable { get; set; }

        /// <summary>
        /// Gets or sets whether the field is the key field. The default is null.
        /// A <see cref="SearchIndex"/> must have exactly one key field of type <see cref="SearchFieldDataType.String"/>.
        /// </summary>
        /// <remarks>
        /// This field must be set according to constraints described in the summary, or the server may respond with an error.
        /// Instead, consider using the <see cref="SimpleField"/>, <see cref="SearchableField"/>, and <see cref="ComplexField"/> classes to help you more easily create a <see cref="SearchIndex"/>.
        /// </remarks>
        [CodeGenMember("key")]
        public bool? IsKey { get; set; }

        // TODO: Remove "overrides" for collection properties when https://github.com/Azure/autorest.csharp/issues/521 is fixed.

        /// <summary>
        /// Gets a list of names of synonym maps associated with this field. Only fields where <see cref="IsSearchable"/> is true can have associated synonym maps.
        /// </summary>
        [CodeGenMember("synonymMaps")]
        public IList<string> SynonymMaps { get; internal set; }

        /// <summary>
        /// Gets a list of nested fields if this field is of type <see cref="SearchFieldDataType.Complex"/> or "Collection(DataType.Complex)".
        /// </summary>
        [CodeGenMember("fields")]
        public IList<SearchField> Fields { get; internal set; }


        /// <inheritdoc/>
        /// <remarks>
        /// This always returns "<see cref="Name"/> : <see cref="Type"/>" and is meant for debugging purposes.
        /// </remarks>
        public override string ToString() => $"{Name} : {Type}";
    }
}
