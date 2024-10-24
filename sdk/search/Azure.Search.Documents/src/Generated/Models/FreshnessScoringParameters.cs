// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.Search.Documents.Models
{
    /// <summary> Provides parameter values to a freshness scoring function. </summary>
    public partial class FreshnessScoringParameters
    {
        /// <summary> Initializes a new instance of FreshnessScoringParameters. </summary>
        /// <param name="boostingDuration"> The expiration period after which boosting will stop for a particular document. </param>
        public FreshnessScoringParameters(TimeSpan boostingDuration)
        {
            BoostingDuration = boostingDuration;
        }

        /// <summary> The expiration period after which boosting will stop for a particular document. </summary>
        public TimeSpan BoostingDuration { get; set; }
    }
}
