
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class Icon
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public int? SettingsID { get; set; }
        public Settings Settings { get; set; }
        public IconType IconType { get; set; }
    }
}
