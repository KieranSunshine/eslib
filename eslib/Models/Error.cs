﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        [JsonPropertyName("error")]
        public string Message { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object>? ExtensionData { get; set; }
    }
}
