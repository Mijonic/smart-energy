using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Call
{
    public class CallNotFoundExcpetion : Exception
    {
        public CallNotFoundExcpetion(string message) : base(message)
        {

        }
    }
}
