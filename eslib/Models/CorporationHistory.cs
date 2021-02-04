using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class CorporationHistory
    {
        public CorporationHistory(
            int corporationId,
            int recordId,
            string startDate)
        {
            CorporationId = corporationId;
            RecordId = recordId;
            StartDate = startDate;
        }
        
        [JsonPropertyName("corporation_id")]
        public int CorporationId { get; set; }
        
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; set; }
        
        [JsonPropertyName("record_id")]
        public int RecordId { get; set; }
        
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }
    }
}