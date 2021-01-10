using eslib.Models;
using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Asset
    {
        [JsonPropertyName("is_blueprint_copy")]
        public bool? IsBlueprintCopy { get; set; }

        [JsonPropertyName("is_singleton")]
        public bool IsSingleton { get; set; }

        [JsonPropertyName("item_id")]
        public long ItemId { get; set; }

        [JsonPropertyName("location_flag")]
        public string LocationFlag { get; set; }

        [JsonPropertyName("location_id")]
        public long LocationId { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }
    }

    public class AssetLocation
    {
        [JsonPropertyName("item_id")]
        public long ItemId { get; set; }

        [JsonPropertyName("position")]
        public Position Postition { get; set; }
    }
}