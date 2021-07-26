using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Resolution
{
    public class InvalidResolutionException : Exception
    {
        public InvalidResolutionException(string message) : base(message)
        {

        }
    }
}
