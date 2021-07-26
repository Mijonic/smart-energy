using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class SafetyDocument
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
        public WorkType DocumentType { get; set; }
        public DocumentStatus DocumentStatus { get; set; }
        public int UserID { get; set; }
        public int? MultimediaAnchorID { get; set; }
        public int? StateChangeAnchorID { get; set; }
        public int? NotificationAnchorID { get; set; }
        public int WorkPlanID { get; set; }
        public User User { get; set; }
        public MultimediaAnchor MultimediaAnchor { get; set; }
        public StateChangeAnchor StateChangeAnchor { get; set; }
        public NotificationAnchor NotificationAnchor { get; set; }
        public WorkPlan WorkPlan { get; set; }
        public List<DeviceUsage> DeviceUsages { get; set; }


        public void Update(SafetyDocument newData)
        {         
         
            Details = newData.Details;
            Notes = newData.Notes;
            Phone = newData.Phone;
            DocumentStatus = newData.DocumentStatus;
            DocumentType = newData.DocumentType;
            WorkPlanID = newData.WorkPlanID;


          
        }

        public void UpdateChecklist(ChecklistDto updated)
        {
            OperationCompleted = updated.OperationCompleted;
            TagsRemoved = updated.TagsRemoved;
            GroundingRemoved = updated.GroundingRemoved;
            Ready = updated.Ready;
      
    }

        //public void UpdateCheckList()
    }
}
