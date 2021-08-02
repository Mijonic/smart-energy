using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IWorkPlanService
    {
        Task<WorkPlanDto> GetWorkPlanById(int workPlanId);

        List<WorkPlanDto> GetAllWorkPlans();
    }
}
