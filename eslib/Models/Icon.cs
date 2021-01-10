﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Icon
    {
        [JsonPropertyName("px128x128")]
        public string Url128px { get; set; }

        [JsonPropertyName("px64x64")]
        public string Url64px { get; set; }
    }
}