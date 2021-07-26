using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Resolution
{
    public class ResolutionNotFoundException : Exception
    {
        public ResolutionNotFoundException(string message) : base(message)
        {

        }
    }
}
