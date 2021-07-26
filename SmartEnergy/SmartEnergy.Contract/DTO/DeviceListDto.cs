using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class DeviceListDto
    {
        public List<DeviceDto> Devices { get; set; }
        public int TotalCount { get; set; }
    }
}
