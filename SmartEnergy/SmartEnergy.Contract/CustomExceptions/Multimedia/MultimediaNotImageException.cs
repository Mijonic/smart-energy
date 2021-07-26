using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Multimedia
{
    public class MultimediaNotImageException : Exception
    {
        public MultimediaNotImageException(string message) : base(message)
        {
        }
    }
}
