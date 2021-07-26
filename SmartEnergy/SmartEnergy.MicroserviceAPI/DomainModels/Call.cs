
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class Call
    {
        public int ID { get; set; }
        public CallReason CallReason { get; set; }
        public string Comment { get; set; }
        public string Hazard { get; set; }
        public int LocationID { get; set; }
        public int? ConsumerID { get; set; }
        public int? IncidentID { get; set; }
       // public Location Location { get; set; }
        public Consumer Consumer { get; set; }
        public Incident Incident { get; set; }


        public void UpdateCall(Call modified)
        {
            IncidentID = modified.IncidentID;


        }
    }
}
