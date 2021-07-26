using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Consumer
{
    public class InvalidConsumerException : Exception
    {
        public InvalidConsumerException(string message) : base(message)
        {
                
        }
    }
}
