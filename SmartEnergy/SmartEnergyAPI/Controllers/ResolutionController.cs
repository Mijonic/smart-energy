using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Resolution;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/resolution")]
    [ApiController]
    public class ResolutionController : ControllerBase
    {
        private readonly IResolutionService _resolutionService;

        public ResolutionController(IResolutionService resolutionService)
        {
            _resolutionService = resolutionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResolutionDto>))]
        public IActionResult GetAllResolutions()
        {
            return Ok(_resolutionService.GetAll());

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResolutionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetResolutionById(int id)
        {
            ResolutionDto resolution = _resolutionService.Get(id);
            if (resolution == null)
                return NotFound();

            return Ok(resolution);
        }

        [HttpGet("incident/{incidentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResolutionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetResolutionIncident(int incidentId)
        {
            ResolutionDto resolution = _resolutionService.GetResolutionIncident(incidentId);
           
            if (resolution == null)
                return NotFound();

            return Ok(resolution);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddResolution([FromBody] ResolutionDto newResolution)
        {
            try
            {
                ResolutionDto resolution = _resolutionService.Insert(newResolution);
                return CreatedAtAction(nameof(GetResolutionById), new { id = resolution.ID }, resolution);
            }
            catch (ResolutionNotFoundException resolutionNotFound)
            {
                return NotFound(resolutionNotFound.Message);
            }
            catch (InvalidResolutionException invalidResolution)
            {
                return BadRequest(invalidResolution.Message);
            }

        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResolutionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateResolution(int id, [FromBody] ResolutionDto modifiedResolution)
        {
            try
            {
                ResolutionDto resolution = _resolutionService.Update(modifiedResolution);

                return Ok(resolution);
            }
            catch(IncidentNotFoundException notFoundException)
            {
                return NotFound(notFoundException.Message);
            }
            catch (ResolutionNotFoundException resolutionNotFound)
            {
                return NotFound(resolutionNotFound.Message);
            }
            catch (InvalidResolutionException invalidResolution)
            {
                return BadRequest(invalidResolution.Message);
            }



        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveResolution(int id)
        {
            try
            {
                _resolutionService.Delete(id);
                return NoContent();
            }
            catch (ResolutionNotFoundException resolutionNotFound)
            {
                return NotFound(resolutionNotFound.Message);
            }
        }



    }
}
