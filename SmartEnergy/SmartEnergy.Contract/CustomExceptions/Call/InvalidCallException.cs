using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Call
{
    public class InvalidCallException : Exception
    {
        public InvalidCallException(string message) : base(message)
        {

        }
    }
}
