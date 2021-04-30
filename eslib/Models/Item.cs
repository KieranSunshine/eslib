using System.Text.Json.Serialization;

namespace Eslib.Models
{
    public class Item
    {
        [JsonPropertyName("item_id")]
        public long? ItemId { get; set; }

        [JsonPropertyName("type_id")]
        public int? TypeId { get; set; }
    }
}