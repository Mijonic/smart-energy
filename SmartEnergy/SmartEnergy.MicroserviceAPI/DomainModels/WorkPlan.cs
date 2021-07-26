using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class WorkPlan
    {
        public int ID { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string CompanyName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public WorkType DocumentType { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public int UserID { get; set; }
        public int? MultimediaAnchorID { get; set; }
        public int? StateChangeAnchorID { get; set; }
        public int? NotificationAnchorID { get; set; }
        public int WorkRequestID { get; set; }
        public User User { get; set; }
        public MultimediaAnchor MultimediaAnchor { get; set; }
        public StateChangeAnchor StateChangeAnchor { get; set; }
        public NotificationAnchor NotificationAnchor { get; set; }
        public WorkRequest WorkRequest { get; set; }
        public List<Instruction> Instructions { get; set; }
        public List<SafetyDocument> SafetyDocuments { get; set; }
        public List<DeviceUsage> WorkPlanDevices { get; set; }
    }
}
