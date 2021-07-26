using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.DeviceUsage
{
    public class InvalidDeviceUsageException : Exception
    {
        public InvalidDeviceUsageException(string message) : base(message)
        {

        }
    }
}
