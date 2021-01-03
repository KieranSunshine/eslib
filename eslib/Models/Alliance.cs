

using System;
using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Alliance
    {
        [JsonPropertyName("creator_corporation_id")]
        public int CreatorCorporationId { get; set; }

        [JsonPropertyName("creator_id")]
        public int CreatorId { get; set; }

        [JsonPropertyName("date_founded")]
        public DateTime DateFounded { get; set; }

        [JsonPropertyName("executor_corporation_id")]
        public int? ExecutorCorporationId { get; set; }

        [JsonPropertyName("faction_id")]
        public int? FactionId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

    }
}
