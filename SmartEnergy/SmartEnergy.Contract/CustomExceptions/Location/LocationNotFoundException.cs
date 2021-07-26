using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Location
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException(string message) : base(message)
        {

        }
    }
}
