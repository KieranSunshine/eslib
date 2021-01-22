using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Asset
    {
        public Asset(string locationFlag, string locationType)
        {
            LocationFlag = locationFlag;
            LocationType = locationType;
        }

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
        public AssetLocation(Position position)
        {
            Position = position;
        }

        [JsonPropertyName("item_id")]
        public long ItemId { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }
    }

    public class AssetName
    {
        public AssetName(long itemId, string name)
        {
            ItemId = itemId;
            Name = name;
        }
        
        [JsonPropertyName("item_id")]
        public long ItemId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}