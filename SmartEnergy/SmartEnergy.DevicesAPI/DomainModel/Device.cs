using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.DevicesAPI.DomainModel
{
    public class Device
    {
        public int ID { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Name { get; set; }
        public int LocationID { get; set; }
        //public Location Location { get; set; }
        //public List<DeviceUsage> DeviceUsage { get; set; }
        //public List<Instruction> Instructions { get; set; }

        public int DeviceCounter { get; set; }

        public DateTime Timestamp { get; set; }

        public void UpdateDevice(Device modified)
        {
            DeviceType = modified.DeviceType;
            Name = modified.Name;
            LocationID = modified.LocationID;
            Timestamp = DateTime.Now;

        }

    }
}
