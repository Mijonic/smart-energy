using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.User
{
    public class InvalidUserDataException : Exception
    {
        public InvalidUserDataException(string message) : base(message)
        {
        }
    }
}
