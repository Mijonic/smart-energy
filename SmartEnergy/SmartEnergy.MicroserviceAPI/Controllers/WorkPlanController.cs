using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions.WorkPlan;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.MicroserviceAPI.Controllers
{
    [Route("api/work-plan")]
    [ApiController]
    public class WorkPlanController : ControllerBase
    {
        private readonly IWorkPlanService _workPlanService;

        public WorkPlanController(IWorkPlanService workPlanService)
        {
            _workPlanService = workPlanService;
        }

        [HttpGet("{id}")]   
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkPlanDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                WorkPlanDto workPlan = await _workPlanService.GetWorkPlanById(id);

                return Ok(workPlan);
            }
            catch (WorkPlanNotFoundException wpnf)
            {
                return NotFound(wpnf.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkPlanDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            try
            {
                List<WorkPlanDto> workPlans =  _workPlanService.GetAllWorkPlans();

                return Ok(workPlans);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


    }
}
