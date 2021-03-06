using System.Text.Json.Serialization;

namespace Eslib.Models
{
    public class EventSummary
    {
        [JsonPropertyName("event_date")]
        public string? EventDate { get; set; }

        [JsonPropertyName("event_id")]
        public int? Id { get; set; }

        [JsonPropertyName("event_response")]
        public Enums.Calendar.EventResponses Response { get; set; }

        [JsonPropertyName("importance")]
        public int? Importance { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}