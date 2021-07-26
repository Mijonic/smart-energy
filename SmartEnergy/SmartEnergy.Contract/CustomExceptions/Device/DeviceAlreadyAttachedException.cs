using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Device
{
    public class DeviceAlreadyAttachedException : Exception
    {
        public DeviceAlreadyAttachedException(string message) : base(message)
        {
        }
    }
}
