using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class EventAttendee
    {
        [JsonPropertyName("character_id")] 
        public int? CharacterId { get; set; }

        [JsonPropertyName("event_response")] 
        public Enums.Calendar.EventResponses Response { get; set; }
    }
}