using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class IncidentDto
    {
        public int ID { get; set; }
        public int? Priority { get; set; }
        public bool Confirmed { get; set; }
        public DateTime ETA { get; set; }
        public DateTime? ATA { get; set; }
        public DateTime? ETR { get; set; }//Check if should be Time or duration?
        public DateTime? IncidentDateTime { get; set; }
        public DateTime? WorkBeginDate { get; set; }
        public double? VoltageLevel { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
       // public User User { get; set; }
       // public Resolution Resolution { get; set; }
        public int? CrewID { get; set; }
       // public Crew Crew { get; set; }
        //public int? MultimediaAnchorID { get; set; }
       // public MultimediaAnchor MultimediaAnchor { get; set; }
      //  public int? NotificationAnchorID { get; set; }
       // public NotificationAnchor NotificationAnchor { get; set; }
        //public List<DeviceUsage> IncidentDevices { get; set; }
        //public List<Call> Calls { get; set; }
        //public WorkRequest WorkRequest { get; set; }
        public string WorkType { get; set; }
        public string IncidentStatus { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
