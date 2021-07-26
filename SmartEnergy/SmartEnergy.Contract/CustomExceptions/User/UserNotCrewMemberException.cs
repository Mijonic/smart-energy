using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions
{
    public class UserNotCrewMemberException : Exception
    {
        public UserNotCrewMemberException(string message) : base(message)
        {
        }
    }
}
