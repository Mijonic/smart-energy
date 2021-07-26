using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.SafetyDocument;
using SmartEnergy.Contract.CustomExceptions.WorkPlan;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.DeviceUsageAPI.Controllers
{
    [Route("api/device-usage")]
    [ApiController]
    public class DeviceUsageController : ControllerBase
    {
        private readonly IDeviceUsageService _deviceUsageService;


        public DeviceUsageController(IDeviceUsageService deviceUsageService)
        {
            _deviceUsageService = deviceUsageService;
        }




        [HttpPut("copy/work-plan/{workPlanId}/safety-document/{safetyDocumentId}")]
       // [Authorize(Roles = "DISPATCHER ", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CopyIncidentDevicesToSafetyDocument(int workPlanId, int safetyDocumentId)
        {
            try
            {
                bool copied = await _deviceUsageService.CopyIncidentDevicesToSafetyDocument(workPlanId, safetyDocumentId);

                return Ok("Successfully copied");
            }
            catch (SafetyDocumentNotFoundException sfnf)
            {
                return NotFound(sfnf.Message);
            }
            catch (WorkPlanNotFoundException wpnf)
            {
                return NotFound(wpnf.Message);
            }
          



        }



        [HttpPut("copy/incident/{incidentId}/work-request/{workRequestID}")]
        // [Authorize(Roles = "DISPATCHER ", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CopyIncidentDevicesToWorkRequest(int incidentID, int workRequestID)
        {
            try
            {
                bool copied = await _deviceUsageService.CopyIncidentDevicesToWorkRequest(incidentID, workRequestID);

                return Ok("Successfully copied");
            }
            catch (WorkRequestNotFound wrnf)
            {
                return NotFound(wrnf.Message);
            }
            catch (IncidentNotFoundException innf)
            {
                return NotFound(innf.Message);
            }




        }



        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveDevice(int id)
        {
            try
            {
                bool condition = await _deviceUsageService.DeleteDeviceUsage(id);
                return NoContent();
            }
            catch (DeviceUsageNotFoundException dunf)
            {
                return NotFound(dunf.Message);
            }
        }

        [HttpGet("{id}")]
        // [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceUsageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeviceUsageById(int id)
        {
            try
            {
                DeviceUsageDto deviceUsage = await _deviceUsageService.GetDeviceUsage(id);

                if (deviceUsage == null)
                    return NotFound();

                return Ok(deviceUsage);
            }
            catch (DeviceUsageNotFoundException dunf)
            {
                return NotFound(dunf.Message);
            }


        }


        [HttpGet]
        //[Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER, ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceUsageDto>))]
        public async Task<IActionResult> GetAllDevices()
        {
            try
            {
                List<DeviceUsageDto> allDeviceUsages = await _deviceUsageService.GetAllDeviceUsages();

                return Ok(allDeviceUsages);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPost]
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDeviceUsage([FromBody] DeviceUsageDto newDevice)
        {
            try
            {
                DeviceUsageDto deviceUsage = await _deviceUsageService.InsertDeviceUsage(newDevice);

                return CreatedAtAction(nameof(GetDeviceUsageById), new { id = deviceUsage.ID }, deviceUsage);
            }
            catch (IncidentNotFoundException inf)
            {
                return NotFound(inf.Message);
            }
            catch (DeviceUsageNotFoundException dunf)
            {
                return NotFound(dunf.Message);
            }

        }


        [HttpPut("{id}")]
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceUsageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDeviceUsage(int id, [FromBody] DeviceUsageDto modifiedDevice)
        {
            try
            {
                DeviceUsageDto deviceUsage = await _deviceUsageService.UpdateDeviceUsage(modifiedDevice);

                return Ok(deviceUsage);
            }
            catch(InvalidDeviceUsageException idu)
            {
                return BadRequest(idu.Message);
            }
            catch (DeviceUsageNotFoundException dunf)
            {
                return NotFound(dunf.Message);
            }
            catch (WorkRequestNotFound wrnf)
            {
                return NotFound(wrnf.Message);
            }
            catch (WorkPlanNotFoundException wpnf)
            {
                return NotFound(wpnf.Message);
            }
            catch (SafetyDocumentNotFoundException sfnf)
            {
                return NotFound(sfnf.Message);
            }



        }


        [HttpPut("work-plan/{workPlanId}/safety-document/{safetyDocumentId}")]
        //[Authorize(Roles = "ADMIN", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeviceUsageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId)
        {
            try
            {
                bool condition = await _deviceUsageService.UpdateSafetyDocumentWorkPlan(workPlanId, safetyDocumentId);

                return Ok();
            }
            catch (WorkPlanNotFoundException wpnf)
            {
                return NotFound(wpnf.Message);
            }
            catch (SafetyDocumentNotFoundException sfnf)
            {
                return NotFound(sfnf.Message);
            }
          


        }


    }
}
