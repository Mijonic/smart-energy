using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IIconService 
    {

        List<IconDto> GetAllIcons();
        IconDto GetIconById(int iconId);
        void AddIconToSettings(int iconId, int settingsId);
        void RemoveIconFromSettings(int iconId, int settingsId);
    }

}
