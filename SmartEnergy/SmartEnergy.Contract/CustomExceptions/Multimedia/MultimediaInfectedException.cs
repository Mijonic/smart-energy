using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Multimedia
{
    public class MultimediaInfectedException : Exception
    {
        public MultimediaInfectedException(string message) : base(message)
        {
        }
    }
}
