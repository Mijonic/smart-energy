using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class ExternalLoginDto
    {

        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
