using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class EventSummary
    {
        [JsonPropertyName("event_date")]
        public string? EventDate { get; set; }

        [JsonPropertyName("event_id")]
        public int? Id { get; set; }

        [JsonPropertyName("event_response")]
        public string? Response { get; set; }

        [JsonPropertyName("importance")]
        public int? Importance { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}