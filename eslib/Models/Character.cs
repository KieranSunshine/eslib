using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Character
    {
        public Character(
            string birthday,
            int bloodlineId,
            int corporationId,
            Enums.Character.Gender gender,
            string name,
            int raceId)
        {
            Birthday = birthday;
            BloodlineId = bloodlineId;
            CorporationId = corporationId;
            Gender = gender;
            Name = name;
            RaceId = raceId;
        }
        
        [JsonPropertyName("alliance_id")]
        public int? AllianceId { get; set; }
        
        [JsonPropertyName("ancestry_id")]
        public int? AncestryId { get; set; }
        
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }
        
        [JsonPropertyName("bloodline_id")]
        public int BloodlineId { get; set; }
        
        [JsonPropertyName("corporation_id")]
        public int CorporationId { get; set; }
        
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        
        [JsonPropertyName("faction_id")]
        public int? FactionId { get; set; }
        
        [JsonPropertyName("gender")]
        public Enums.Character.Gender Gender { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("race_id")]
        public int RaceId { get; set; }
        
        [JsonPropertyName("security_status")]
        public float? SecurityStatus { get; set; }
        
        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
