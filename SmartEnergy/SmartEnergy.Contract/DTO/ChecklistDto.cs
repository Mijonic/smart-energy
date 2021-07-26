using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class ChecklistDto
    {
        public int SafetyDocumentId { get; set; }
        public bool OperationCompleted { get; set; }
        public bool TagsRemoved { get; set; }
        public bool GroundingRemoved { get; set; }
        public bool Ready { get; set; }
    }
}
