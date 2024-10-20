// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.Search.Documents.Models
{
    internal static class StopwordsListExtensions
    {
        public static string ToSerialString(this StopwordsList value) => value switch
        {
            StopwordsList.Arabic => "arabic",
            StopwordsList.Armenian => "armenian",
            StopwordsList.Basque => "basque",
            StopwordsList.Brazilian => "brazilian",
            StopwordsList.Bulgarian => "bulgarian",
            StopwordsList.Catalan => "catalan",
            StopwordsList.Czech => "czech",
            StopwordsList.Danish => "danish",
            StopwordsList.Dutch => "dutch",
            StopwordsList.English => "english",
            StopwordsList.Finnish => "finnish",
            StopwordsList.French => "french",
            StopwordsList.Galician => "galician",
            StopwordsList.German => "german",
            StopwordsList.Greek => "greek",
            StopwordsList.Hindi => "hindi",
            StopwordsList.Hungarian => "hungarian",
            StopwordsList.Indonesian => "indonesian",
            StopwordsList.Irish => "irish",
            StopwordsList.Italian => "italian",
            StopwordsList.Latvian => "latvian",
            StopwordsList.Norwegian => "norwegian",
            StopwordsList.Persian => "persian",
            StopwordsList.Portuguese => "portuguese",
            StopwordsList.Romanian => "romanian",
            StopwordsList.Russian => "russian",
            StopwordsList.Sorani => "sorani",
            StopwordsList.Spanish => "spanish",
            StopwordsList.Swedish => "swedish",
            StopwordsList.Thai => "thai",
            StopwordsList.Turkish => "turkish",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown StopwordsList value.")
        };

        public static StopwordsList ToStopwordsList(this string value)
        {
            if (string.Equals(value, "arabic", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Arabic;
            if (string.Equals(value, "armenian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Armenian;
            if (string.Equals(value, "basque", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Basque;
            if (string.Equals(value, "brazilian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Brazilian;
            if (string.Equals(value, "bulgarian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Bulgarian;
            if (string.Equals(value, "catalan", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Catalan;
            if (string.Equals(value, "czech", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Czech;
            if (string.Equals(value, "danish", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Danish;
            if (string.Equals(value, "dutch", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Dutch;
            if (string.Equals(value, "english", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.English;
            if (string.Equals(value, "finnish", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Finnish;
            if (string.Equals(value, "french", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.French;
            if (string.Equals(value, "galician", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Galician;
            if (string.Equals(value, "german", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.German;
            if (string.Equals(value, "greek", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Greek;
            if (string.Equals(value, "hindi", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Hindi;
            if (string.Equals(value, "hungarian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Hungarian;
            if (string.Equals(value, "indonesian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Indonesian;
            if (string.Equals(value, "irish", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Irish;
            if (string.Equals(value, "italian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Italian;
            if (string.Equals(value, "latvian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Latvian;
            if (string.Equals(value, "norwegian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Norwegian;
            if (string.Equals(value, "persian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Persian;
            if (string.Equals(value, "portuguese", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Portuguese;
            if (string.Equals(value, "romanian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Romanian;
            if (string.Equals(value, "russian", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Russian;
            if (string.Equals(value, "sorani", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Sorani;
            if (string.Equals(value, "spanish", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Spanish;
            if (string.Equals(value, "swedish", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Swedish;
            if (string.Equals(value, "thai", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Thai;
            if (string.Equals(value, "turkish", StringComparison.InvariantCultureIgnoreCase)) return StopwordsList.Turkish;
            throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown StopwordsList value.");
        }
    }
}
