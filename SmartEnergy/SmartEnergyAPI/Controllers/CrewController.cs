
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/crews")]
    [ApiController]
    public class CrewController : ControllerBase
    {
        private readonly ICrewService _crewService;

        public CrewController(ICrewService crewService)
        {
            _crewService = crewService;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CrewDto>))]
        public IActionResult GetCrews()
        {
            return Ok(_crewService.GetAll());

        }

        [HttpGet]
        [Authorize(Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CrewsListDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCrewsPaged([FromQuery] CrewField sortBy, [FromQuery] SortingDirection direction, [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage)
        {
            return Ok(_crewService.GetCrewsPaged(sortBy, direction, page, perPage));

        }


        [HttpGet("{id}")]
        [Authorize(Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CrewDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCrewById(int id)
        {
            CrewDto crew = _crewService.Get(id);
            if (crew == null)
                return NotFound();
            return Ok(crew);
        }


        [HttpPost]
        [Authorize(Roles ="ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddCrew([FromBody] CrewDto newCrew)
        {
            try
            {
                CrewDto crew = _crewService.Insert(newCrew);
                return CreatedAtAction(nameof(GetCrewById), new { id = crew.ID }, crew);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (CrewNotFoundException cnf)
            {
                return NotFound(cnf.Message);
            }
            catch (UserNotCrewMemberException uncm)
            {
                return BadRequest(uncm.Message);
            }
            catch (UserAlreadyInCrewException uaic)
            {
                return BadRequest(uaic.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CrewDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateCrew(int id, [FromBody] CrewDto modifiedCrew)
        {
            try
            {
                CrewDto crew = _crewService.Update(modifiedCrew);
                return Ok(crew);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (CrewNotFoundException cnf)
            {
                return NotFound(cnf.Message);
            }
            catch (UserNotCrewMemberException uncm)
            {
                return BadRequest(uncm.Message);
            }
            catch (UserAlreadyInCrewException uaic)
            {
                return BadRequest(uaic.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult RemoveCrew(int id)
        {
            try
            {
                _crewService.Delete(id);
                return NoContent();
            }
            catch (CrewNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
