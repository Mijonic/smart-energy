using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class WorkRequestsListDto
    {
        public List<WorkRequestDto> WorkRequests { get; set; }
        public int TotalCount { get; set; }
    }
}
