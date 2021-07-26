using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class SettingsDto
    {
        public int ID { get; set; }
        public bool ShowErrors { get; set; }
        public bool ShowWarnings { get; set; }
        public bool ShowInfo { get; set; }
        public bool ShowSuccess { get; set; }
        public bool ShowNonRequiredFields { get; set; }
        public bool IsDefault { get; set; }
      
    }
}
