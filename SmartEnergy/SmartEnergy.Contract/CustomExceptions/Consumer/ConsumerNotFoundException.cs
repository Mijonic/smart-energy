using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Consumer
{
    public class ConsumerNotFoundException : Exception
    {
        public ConsumerNotFoundException(string message) : base(message)
        {

        }
    }
}
