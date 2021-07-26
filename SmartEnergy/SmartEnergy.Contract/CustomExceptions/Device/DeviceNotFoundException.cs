using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Device
{
    public class DeviceNotFoundException : Exception
    {
        public DeviceNotFoundException(string message) : base(message)
        {

        }
    }
}
