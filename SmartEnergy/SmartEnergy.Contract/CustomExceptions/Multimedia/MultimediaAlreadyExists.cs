using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Multimedia
{
    public class MultimediaAlreadyExists : Exception
    {
        public MultimediaAlreadyExists(string message) : base(message)
        {
        }
    }
}
