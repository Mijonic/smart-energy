using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

       
        [HttpGet("all")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public IActionResult GetAllDevices()
        {
            return Ok(_deviceService.GetAll());

        }


        [HttpGet]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public IActionResult GetDevicesPaged([FromQuery] DeviceField sortBy, [FromQuery] SortingDirection direction,
                                  [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage)
        {
            return Ok(_deviceService.GetDevicesPaged(sortBy, direction, page, perPage));
        }

        [HttpGet("search")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public IActionResult GetSearchDevicesPaged([FromQuery] DeviceField sortBy, [FromQuery] SortingDirection direction,
                                 [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage, [FromQuery] DeviceFilter type,
                                 [FromQuery] DeviceField field, [FromQuery] string searchParam)
        {
            return Ok(_deviceService.GetSearchDevicesPaged(sortBy, direction, page, perPage, type, field, searchParam));
        }
       



        [HttpGet("{id}")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDeviceById(int id)
        {
            DeviceDto device = _deviceService.Get(id);
            if (device == null)
                return NotFound();

            return Ok(device);
        }


        [HttpPost]
        [Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddDevice([FromBody] DeviceDto newDevice)
        {
            try
            {
                DeviceDto device = _deviceService.Insert(newDevice);
                return CreatedAtAction(nameof(GetDeviceById), new { id = device.ID }, device);
            }
            catch (InvalidDeviceException invalidDevice)
            {
                return BadRequest(invalidDevice.Message);
            }
            catch (LocationNotFoundException locationNotFound)
            {
                return NotFound(locationNotFound.Message);
            }
          
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDevice(int id, [FromBody] DeviceDto modifiedDevice)
        {
            try
            {
                DeviceDto device = _deviceService.Update(modifiedDevice);
                return Ok(device);
            }
            catch (InvalidDeviceException invalidDevice)
            {
                return BadRequest(invalidDevice.Message);
            }
            catch (DeviceNotFoundException deviceNotFound)
            {
                return NotFound(deviceNotFound.Message);
            }
            catch (LocationNotFoundException locationNotFound)
            {
                return NotFound(locationNotFound.Message);
            }
           
           

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveDevice(int id)
        {
            try
            {
                _deviceService.Delete(id);
                return NoContent();
            }
            catch (DeviceNotFoundException deviceNotFound)
            {
                return NotFound(deviceNotFound.Message);
            }
        }







    }
}
