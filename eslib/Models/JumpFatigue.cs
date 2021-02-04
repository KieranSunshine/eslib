using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class JumpFatigue
    {
        [JsonPropertyName("jump_fatigue_expire_date")]
        public string? ExpiryDate { get; set; }
        
        [JsonPropertyName("last_jump_date")]
        public string? LastJumpDate { get; set; }
        
        [JsonPropertyName("last_update_date")]
        public string? LastUpdateDate { get; set; }
    }
}