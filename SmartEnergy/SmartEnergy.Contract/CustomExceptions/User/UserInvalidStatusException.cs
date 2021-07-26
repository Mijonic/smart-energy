using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.User
{
    public class UserInvalidStatusException : Exception
    {
        public UserInvalidStatusException(string message) : base(message)
        {
        }
    }
}
