using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class Settings
    {
        public int ID { get; set; }
        public bool? ShowErrors { get; set; }
        public bool? ShowWarnings { get; set; }
        public bool? ShowInfo { get; set; }
        public bool? ShowSuccess { get; set; }
        public bool? ShowNonRequiredFields { get; set; }
        public bool? IsDefault {get; set;}
        public List<Icon> Icons { get; set; }


        public void UpdateSetting(Settings modified)
        {
            ShowErrors = modified.ShowErrors;
            ShowWarnings = modified.ShowWarnings;
            ShowInfo = modified.ShowInfo;
            ShowSuccess = modified.ShowSuccess;
            ShowNonRequiredFields = modified.ShowNonRequiredFields;
            IsDefault = false;

        }

    }
}
