using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.SafetyDocument
{
    public class InvalidSafetyDocumentException : Exception
    {
        public InvalidSafetyDocumentException(string message) : base(message)
        {

        }
    }
}
