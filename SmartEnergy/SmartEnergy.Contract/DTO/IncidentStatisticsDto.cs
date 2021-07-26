using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class IncidentStatisticsDto
    {

        public int Unresolved { get; set; }
        public int Resolved { get; set; }
        public int Total { get; set; }
        public int Planned { get; set; }
        public int Unplanned { get; set; }
    }
}
