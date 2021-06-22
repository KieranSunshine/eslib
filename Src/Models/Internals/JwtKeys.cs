using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eslib.Models.Internals
{
    public struct JwtKeys
    {
        public IEnumerable<IJwtKey> Keys { get; set; }
    }

    public interface IJwtKey
    {
        [JsonPropertyName("alg")]
        public string Algorithm { get; set; }
        
        [JsonPropertyName("kid")]
        public string KeyId { get; set; }
        
        [JsonPropertyName("kty")]
        public string Type { get; set; }
    }
}