using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Incident
{
    public class IncidentNotFoundException : Exception
    {
        public IncidentNotFoundException(string message) : base(message)
        {
        }
    }
}
