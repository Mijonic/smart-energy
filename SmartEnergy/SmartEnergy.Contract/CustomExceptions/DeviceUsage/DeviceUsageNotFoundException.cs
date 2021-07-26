using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.DeviceUsage
{
    public class DeviceUsageNotFoundException : Exception
    {
        public DeviceUsageNotFoundException(string message) : base(message)
        {

        }
    }
}
