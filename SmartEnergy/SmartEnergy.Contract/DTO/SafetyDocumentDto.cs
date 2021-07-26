using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class SafetyDocumentDto
    {
        public int ID { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public bool OperationCompleted { get; set; }
        public bool TagsRemoved { get; set; }
        public bool GroundingRemoved { get; set; }
        public bool Ready { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DocumentType { get; set; }
        public string DocumentStatus { get; set; }
        public int UserID { get; set; }
        public int? MultimediaAnchorID { get; set; }
        public int? StateChangeAnchorID { get; set; }
        public int? NotificationAnchorID { get; set; }
        public int WorkPlanID { get; set; }
        public UserDto User { get; set; }

        public string CrewName { get; set; }
        //public MultimediaAnchor MultimediaAnchor { get; set; }
        //public StateChangeAnchor StateChangeAnchor { get; set; }
        //public NotificationAnchor NotificationAnchor { get; set; }
        //public WorkPlan WorkPlan { get; set; }
        //public List<DeviceUsage> DeviceUsages { get; set; }
    }
}
