using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DeviceUsageAPI.DomainModel
{
    public class DeviceUsage
    {
        public int ID { get; set; }
        public int? IncidentID { get; set; }
      
        public int? WorkRequestID { get; set; }
     
        public int? WorkPlanID { get; set; }
   
        public int? SafetyDocumentID { get; set; }
     
        public int DeviceID { get; set; }
      

        public void UpdateDeviceUsage(DeviceUsage modified)
        {
            WorkRequestID = modified.WorkRequestID;
            WorkPlanID = modified.WorkPlanID;
            SafetyDocumentID = modified.SafetyDocumentID;
            DeviceID = modified.DeviceID;
        }
    }
}
