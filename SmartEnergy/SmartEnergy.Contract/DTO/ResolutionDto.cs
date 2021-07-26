using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class ResolutionDto
    {
        public int ID { get; set; }
        public string Cause { get; set; }
        public string Subcause { get; set; }
        public string Construction { get; set; }
        public string Material { get; set; }

        public int IncidentID { get; set; }

        //public IncidentDto Incident { get; set; } 
    }
}
