using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class IncidentMapDisplayDto
    {
        public int ID { get; set; }
        public int? Priority { get; set; }
        public DateTime? IncidentDateTime { get; set; }
        public LocationDto Location { get; set; }
        public CrewDto Crew { get; set; }
    }
}
