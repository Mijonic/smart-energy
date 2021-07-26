using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.SafetyDocument
{
    public class SafetyDocumentNotFoundException : Exception
    {
        public SafetyDocumentNotFoundException(string message) : base(message)
        {

        }
    }
}
