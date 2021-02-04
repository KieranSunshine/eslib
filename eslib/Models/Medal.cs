using System;
using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Medal
    {
        public Medal(
            string corporationId,
            string date,
            string description,
            Graphics[] graphics,
            int issuerId,
            int medalId,
            string reason,
            Enums.Medals.Status status,
            string title)
        {
            if (graphics.Length < 3 || graphics.Length > 9)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(graphics),
                    "graphics array cannot be less than 3 or greater than 9");
            }
            
            CorporationId = corporationId;
            Date = date;
            Description = description;
            Graphics = graphics;
            IssuerId = issuerId;
            MedalId = medalId;
            Reason = reason;
            Status = status;
            Title = title;
        }
        
        [JsonPropertyName("corporation_id")]
        public string CorporationId { get; set; }
        
        [JsonPropertyName("date")]
        public string Date { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("graphics")]
        public Graphics[] Graphics { get; set; }
        
        [JsonPropertyName("issuer_id")]
        public int IssuerId { get; set; }
        
        [JsonPropertyName("medal_id")]
        public int MedalId { get; set; }
        
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
        
        [JsonPropertyName("status")]
        public Enums.Medals.Status Status { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}