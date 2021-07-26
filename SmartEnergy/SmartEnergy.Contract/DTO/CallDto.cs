using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class CallDto
    {
        public int ID { get; set; }
        public string CallReason { get; set; }
        public string Comment { get; set; }
        public string Hazard { get; set; }
        public int LocationID { get; set; }
        public int? ConsumerID { get; set; }
        public int? IncidentID { get; set; }     
        public LocationDto Location { get; set; }
        public ConsumerDto Consumer { get; set; }
       // public int Incident { get; set; }

    }
}
