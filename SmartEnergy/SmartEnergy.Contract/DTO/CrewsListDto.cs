using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class CrewsListDto
    {
        public List<CrewDto> Crews { get; set; }
        public int TotalCount { get; set; }
    }
}
