using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class StateChangeHistory
    {
        public int ID { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DocumentStatus DocumentStatus { get; set;}
        public int StateChangeAnchorID { get; set; }
        public StateChangeAnchor StateChangeAnchor { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
