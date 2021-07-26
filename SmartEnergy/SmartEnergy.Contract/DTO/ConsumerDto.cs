using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class ConsumerDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string AccountID { get; set; }         
        public int LocationID { get; set; }

        public LocationDto Location { get; set; }
        public string AccountType { get; set; }
    }
}
