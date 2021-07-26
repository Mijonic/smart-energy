using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class Instruction
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public bool IsExecuted { get; set; }
        public int DeviceID { get; set; }
        public int WorkPlanID { get; set; }

        //public Device Device { get; set; }

        public WorkPlan WorkPlan { get; set; }
    }
}
