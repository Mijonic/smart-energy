using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Crew
{
    public class InvalidCrewException : Exception
    {
        public InvalidCrewException(string message) : base(message)
        {
        }
    }
}
