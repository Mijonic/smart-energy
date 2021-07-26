using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Incident
{
    public class InvalidIncidentException : Exception
    {
        public InvalidIncidentException(string message) : base(message)
        {

        }
    }
}
