using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.WorkRequest
{
    public class WorkRequestNotFound : Exception
    {
        public WorkRequestNotFound(string message) : base(message)
        {
        }
    }
}
