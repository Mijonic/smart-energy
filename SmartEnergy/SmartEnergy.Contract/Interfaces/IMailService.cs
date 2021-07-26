using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IMailService
    {
        void SendMail(string to, string subject, string text);
    }
}
