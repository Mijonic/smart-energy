using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Multimedia
{
    public class MultimediaNotFoundException : Exception
    {
        public MultimediaNotFoundException(string message) : base(message)
        {
        }
    }
}
