using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Event
    {
        public Event(
            string date,
            int duration,
            int eventId,
            int importance,
            int ownerId,
            string ownerName,
            string ownerType,
            string response,
            string text,
            string title)
        {
            Date = date;
            Duration = duration;
            EventId = eventId;
            Importance = importance;
            OwnerId = ownerId;
            OwnerName = ownerName;
            OwnerType = ownerType;
            Response = response;
            Text = text;
            Title = title;
        }

        [JsonPropertyName("date")] 
        public string Date { get; set; }

        [JsonPropertyName("duration")] 
        public int Duration { get; set; }

        [JsonPropertyName("event_id")] 
        public int EventId { get; set; }

        [JsonPropertyName("importance")] 
        public int Importance { get; set; }

        [JsonPropertyName("owner_id")] 
        public int OwnerId { get; set; }

        [JsonPropertyName("owner_name")] 
        public string OwnerName { get; set; }

        [JsonPropertyName("owner_type")] 
        public string OwnerType { get; set; }

        [JsonPropertyName("response")] 
        public string Response { get; set; }

        [JsonPropertyName("text")] 
        public string Text { get; set; }

        [JsonPropertyName("title")] 
        public string Title { get; set; }
    }
}