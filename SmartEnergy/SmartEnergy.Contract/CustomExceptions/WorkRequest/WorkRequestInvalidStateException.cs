using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.WorkRequest
{
    public class WorkRequestInvalidStateException : Exception
    {
        public WorkRequestInvalidStateException(string message) : base(message)
        {
        }
    }
}
