using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class WorkRequest
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedOn{ get; set; }
        public string Purpose { get; set; }
        public string Note{ get; set; }
        public string Details { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public WorkType DocumentType { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public bool IsEmergency { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int IncidentID { get; set; }
        public Incident Incident { get; set; }
        public int? MultimediaAnchorID { get; set; }
        public MultimediaAnchor MultimediaAnchor { get; set; }
        public int? StateChangeAnchorID { get; set; }
        public StateChangeAnchor StateChangeAnchor { get; set; }
        public int? NotificationAnchorID { get; set; }
        public NotificationAnchor NotificationsAnchor { get; set; }
        public List<DeviceUsage> DeviceUsage { get; set; }
        public WorkPlan WorkPlan { get; set; }

        public void Update(WorkRequest newData)
        {
            StartDate = newData.StartDate;
            EndDate = newData.EndDate;
            Purpose = newData.Purpose;
            Note = newData.Note;
            Details = newData.Details;
            CompanyName = newData.CompanyName;
            Phone = newData.Phone;
            Street = newData.Street;
            DocumentType = newData.DocumentType;
            IncidentID = newData.IncidentID;
            IsEmergency = newData.IsEmergency;
        }

    }
}
