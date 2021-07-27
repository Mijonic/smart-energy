using AutoMapper;
using SmartEnergy.Contract.CustomExceptions.WorkPlan;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.MicroserviceAPI.DomainModels;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class WorkPlanService : IWorkPlanService
    {
        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;

        public WorkPlanService(MicroserviceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WorkPlanDto> GetWorkPlanById(int id)
        {
            WorkPlan workPlan =  await _dbContext.WorkPlans.FindAsync(id);

            if (workPlan == null)
                throw new WorkPlanNotFoundException($"Work plan with id {id} does not exist.");

            return _mapper.Map<WorkPlanDto>(workPlan);
        }
    }
}
