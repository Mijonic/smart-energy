using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.DTO
{
    public class IconDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? SettingsID { get; set; }
      
        public IconType IconType { get; set; }
    }
}
