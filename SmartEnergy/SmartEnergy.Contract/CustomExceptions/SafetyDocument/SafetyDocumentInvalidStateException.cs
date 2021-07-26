using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.WorkRequest
{
    public class SafetyDocumentInvalidStateException : Exception
    {
        public SafetyDocumentInvalidStateException(string message) : base(message)
        {

        }
    }
}
