using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class IncidentListDto
    {
        public List<IncidentDto> Incidents { get; set; }
        public int TotalCount { get; set; }
    }
}
