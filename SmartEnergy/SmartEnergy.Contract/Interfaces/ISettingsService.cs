using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ISettingsService 
    {

        SettingsDto GetDefaultSettings();

        SettingsDto GetLastSettings();
        void ResetToDefault();

        SettingsDto UpdateSettings(SettingsDto modified);
      


    }
}
