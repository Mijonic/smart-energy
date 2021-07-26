using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class MultimediaAnchor
    {
        public int ID { get; set; }

        public Incident Incident { get; set;  }
        public WorkRequest WorkRequest { get; set; }
        public WorkPlan WorkPlan { get; set; }
        public SafetyDocument SafetyDocument { get; set; }
        public List<MultimediaAttachment> MultimediaAttachments { get; set; }

    }
}
