using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class WorkRequestStatisticsDto
    {
        public int Total { get; set; }
        public int Approved { get; set; }
        public int Cancelled { get; set; }
        public int Denied { get; set; }
        public int Draft { get; set; }
    }
}
