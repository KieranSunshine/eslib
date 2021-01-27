using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class EventResponse
    {
        [JsonPropertyName("character_id")] public int? CharacterId { get; set; }

        [JsonPropertyName("event_response")] public string? Response { get; set; }
    }
}