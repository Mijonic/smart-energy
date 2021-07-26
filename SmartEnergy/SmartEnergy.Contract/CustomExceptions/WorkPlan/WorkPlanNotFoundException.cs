using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.WorkPlan
{
    public class WorkPlanNotFoundException : Exception
    {
        public WorkPlanNotFoundException(string message) : base(message)
        {

        }
    }
}
