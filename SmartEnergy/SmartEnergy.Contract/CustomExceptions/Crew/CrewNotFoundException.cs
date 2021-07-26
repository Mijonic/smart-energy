using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions
{
    public class CrewNotFoundException : Exception
    {
        public CrewNotFoundException(string message) : base(message)
        {
        }
    }
}
