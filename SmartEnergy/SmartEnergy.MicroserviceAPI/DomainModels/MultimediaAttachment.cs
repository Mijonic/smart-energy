using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class MultimediaAttachment
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public int MultimediaAnchorID { get; set; }
        public MultimediaAnchor MultimediaAnchor { get; set; }
    }
}
