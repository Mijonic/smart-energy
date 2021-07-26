using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class TimeService : ITimeService
    {
        public DayPeriod GetCurrentDayPeriod()
        {
            DateTime currentTime = DateTime.Now;

            if ((currentTime.Hour * 60 + currentTime.Minute > 6 * 60) && (currentTime.Hour * 60 + currentTime.Minute <= 12 * 60))
            {
                return DayPeriod.MORNING;
            }
            else if ((currentTime.Hour * 60 + currentTime.Minute > 12 * 60) && (currentTime.Hour * 60 + currentTime.Minute <= 23 * 60))
            {
                return DayPeriod.NOON;
            }
            else
            {
                return DayPeriod.NIGHT;
            }

        }
    }
}
