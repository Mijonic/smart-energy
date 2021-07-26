using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.DevicesAPI.Controllers
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
        //[Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        public async Task<IActionResult> GetAllDevices()
        {
            try
            {
                List<DeviceDto> allDevices = await _deviceService.GetAllDevices();

                return Ok(allDevices);

            }catch(LocationNotFoundException locnf)
            {
                return NotFound(locnf.Message);
            }

        }


        [HttpGet]
       // [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceListDto))]
        public async Task<IActionResult> GetDevicesPaged([FromQuery] DeviceField sortBy, [FromQuery] SortingDirection direction,
                                  [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage)
        {
            try
            {
                DeviceListDto devicesPaged = await _deviceService.GetDevicesPaged(sortBy, direction, page, perPage);

                return Ok(devicesPaged);
            }
            catch(LocationNotFoundException lnf)
            {
                return NotFound(lnf.Message);
            }
        }

        [HttpGet("search")]
       // [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceListDto))]
        public async Task<IActionResult> GetSearchDevicesPaged([FromQuery] DeviceField sortBy, [FromQuery] SortingDirection direction,
                                 [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage, [FromQuery] DeviceFilter type,
                                 [FromQuery] DeviceField field, [FromQuery] string searchParam)
        {

            try
            {
                DeviceListDto searchedDevices = await _deviceService.GetSearchDevicesPaged(sortBy, direction, page, perPage, type, field, searchParam);

                return Ok(searchedDevices);

            }
            catch(LocationNotFoundException lnf)
            {
                return NotFound(lnf.Message);
            }
        }




        [HttpGet("{id}")]
       // [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            try
            {
                DeviceDto device = await _deviceService.GetById(id);

                if (device == null)
                    return NotFound();

                return Ok(device);
            }
            catch (LocationNotFoundException locnf)
            {
                return NotFound(locnf.Message);
            }

          
        }


        [HttpPost]
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDevice([FromBody] DeviceDto newDevice)
        {
            try
            {
                DeviceDto device = await _deviceService.InsertDevice(newDevice);

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
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDevice(int id, [FromBody] DeviceDto modifiedDevice)
        {
            try
            {
                DeviceDto device =  await _deviceService.UpdateDevice(modifiedDevice);
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
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
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
