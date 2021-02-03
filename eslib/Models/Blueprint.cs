using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class Blueprint
    {
        public Blueprint(
            long itemId,
            Enums.Locations.LocationFlags locationFlag,
            long locationId,
            int materialEfficiency,
            int quantity,
            int runs,
            int timeEfficiency,
            int typeId)
        {
            ItemId = itemId;
            LocationFlag = locationFlag;
            LocationId = locationId;
            MaterialEfficiency = materialEfficiency;
            Quantity = quantity;
            Runs = runs;
            TimeEfficiency = timeEfficiency;
            TypeId = typeId;
        }
        
        [JsonPropertyName("blueprint_id")]
        public long ItemId { get; set; }
        
        [JsonPropertyName("location_flag")]
        public Enums.Locations.LocationFlags LocationFlag { get; set; }
        
        [JsonPropertyName("location_id")]
        public long LocationId { get; set; }
        
        [JsonPropertyName("material_efficiency")]
        public int MaterialEfficiency { get; set; }
        
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        
        [JsonPropertyName("runs")]
        public int Runs { get; set; }
        
        [JsonPropertyName("time_efficiency")]
        public int TimeEfficiency { get; set; }
        
        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }
    }
}