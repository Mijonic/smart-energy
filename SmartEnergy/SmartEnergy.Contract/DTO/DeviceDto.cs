using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class DeviceDto
    {
        public int ID { get; set; }
        public string DeviceType { get; set; }
        public string Name { get; set; }
        public int LocationID { get; set; }
        public LocationDto Location { get; set; }

        public int DeviceCounter { get; set; }

        public DateTime Timestamp { get; set; }

        //public List<DeviceUsage> DeviceUsage { get; set; }
        // public List<Instruction> Instructions { get; set; }
    }
}
