using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

namespace SmartEnergy.MicroserviceAPI.Controllers
{
    [Route("api/consumers")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumerService;

        public ConsumerController(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ConsumerDto>))]
        public async Task<IActionResult> GetConsumers()
        {
            try
            {
                List<ConsumerDto> consumers = await _consumerService.GetAllConsumers();

                return Ok(consumers);

            }
            catch(LocationNotFoundException lnf)
            {
                return NotFound(lnf.Message);
            }

        }
    }
}
