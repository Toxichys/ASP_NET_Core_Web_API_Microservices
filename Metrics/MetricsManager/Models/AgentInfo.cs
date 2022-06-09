using System;

namespace MetricsManager.Models
{
    public class AgentInfo
    {
        public int AgentId { get; set; }
        public Uri AgentAddress { get; set; }
        public bool Enable { get; set; }
        public override string ToString()
        {
            return $"ID {AgentId} - adress {AgentAddress} - enable {Enable}";
        }
    }
}
