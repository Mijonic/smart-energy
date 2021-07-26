using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class StateChangeAnchor
    {
        public int ID { get; set; }
        public WorkRequest WorkRequest { get; set; }
        public WorkPlan WorkPlan { get; set; }
        public SafetyDocument SafetyDocument { get; set; }
        public List<StateChangeHistory> StateChangeHistories { get; set; }
    }
}
