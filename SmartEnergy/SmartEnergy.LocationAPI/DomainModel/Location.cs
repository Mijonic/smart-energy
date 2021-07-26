using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.LocationAPI.DomainModel
{
    public class Location
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Number { get; set; }
        public string Zip { get; set; }
        public int MorningPriority { get; set; }
        public int NoonPriority { get; set; }
        public int NightPriority { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        //public List<Consumer> Consumers { get; set; }
        //public List<User> Users { get; set; }
        //public List<Call> Calls { get; set; }
        //public List<Device> Devices { get; set; }
    }
}
