using System.Text.Json.Serialization;

namespace eslib.Models
{
    public class AgentResearch
    {
        public AgentResearch(
            int agentId,
            double pointsPerDay,
            double remainderPoints,
            int skillTypeId,
            string startedAt)
        {
            AgentId = agentId;
            PointsPerDay = pointsPerDay;
            RemainderPoints = remainderPoints;
            SkillTypeId = skillTypeId;
            StartedAt = startedAt;
        }
        
        [JsonPropertyName("agent_id")]
        public int AgentId { get; }
        
        [JsonPropertyName("points_per_day")]
        public double PointsPerDay { get; }
        
        [JsonPropertyName("remainder_points")]
        public double RemainderPoints { get; }
        
        [JsonPropertyName("skill_type_id")]
        public int SkillTypeId { get; }
        
        [JsonPropertyName("started_at")]
        public string StartedAt { get; }
    }
}