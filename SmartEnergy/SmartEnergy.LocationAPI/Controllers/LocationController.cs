
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartEnergy.LocationAPI.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly DaprClient _daprClient;

        public LocationController(ILocationService locationService, DaprClient daprClient)
        {
            _locationService = locationService;
            _daprClient = daprClient;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LocationDto>))]
        public async Task<IActionResult> GetAll()
        {
            var test = await _daprClient.InvokeMethodAsync<List<DeviceDto>>(HttpMethod.Get, "smartenergydevice", "/api/devices/all");
         
            return Ok(_locationService.GetAllLocations());
        }

        [HttpGet("{id}")]      
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLocationById(int id)
        {
            LocationDto location = _locationService.GetLocationById(id);

            if (location == null)
                return NotFound();

            return Ok(location);
        }

    }
}
