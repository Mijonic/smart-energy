using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class CrewDto
    {
        public int ID { get; set; }
        public string CrewName { get; set; }
        public List<UserDto> CrewMembers { get; set; }
    }
}
