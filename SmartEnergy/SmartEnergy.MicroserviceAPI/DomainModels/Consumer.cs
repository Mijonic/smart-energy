using SmartEnergy.Contract.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class Consumer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string AccountID { get; set; }
        public AccountType AccountType { get; set; }
        public int? UserID { get; set; }
        public User User { get; set; }
        public int LocationID { get; set; }
       // public Location Location { get; set; }
        public List<Call> Calls { get; set; }

    }
}
