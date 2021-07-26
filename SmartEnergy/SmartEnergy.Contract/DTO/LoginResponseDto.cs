using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public bool IsSuccessfull { get; set; }
        public UserDto User { get; set; }
    }
}
