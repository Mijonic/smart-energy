using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Device
{
    public class InvalidDeviceException : Exception
    {
        public InvalidDeviceException(string message) : base(message)
        {

        }
    }
}
