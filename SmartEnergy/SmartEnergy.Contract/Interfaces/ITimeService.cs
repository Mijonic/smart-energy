using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ITimeService
    {
        DayPeriod GetCurrentDayPeriod();
    }
}
