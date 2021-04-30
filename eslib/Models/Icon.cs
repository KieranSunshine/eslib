using System.Text.Json.Serialization;

namespace Eslib.Models
{
    public class Icon
    {
        [JsonPropertyName("px128x128")] 
        public string? Url128 { get; set; }

        [JsonPropertyName("px64x64")] 
        public string? Url64 { get; set; }
    }
}