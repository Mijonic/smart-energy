using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions
{
    public class UserAlreadyInCrewException : Exception
    {
        public UserAlreadyInCrewException(string message) : base(message)
        {
        }
    }
}
