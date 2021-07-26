using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class StateChangeHistoryDto
    {
        public int ID { get; set; }
        public DateTime ChangeDate { get; set; }
        public string  DocumentStatus { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
