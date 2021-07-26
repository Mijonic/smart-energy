using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Location
{
    public class InvalidLocationException : Exception
    {
        public InvalidLocationException(string message) : base(message)
        {

        }
    }
}
