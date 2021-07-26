using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Multimedia;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartEnergyAPI.Controllers
{
    [Route("api/work-requests")]
    [ApiController]
    public class WorkRequestController : ControllerBase
    {
        private readonly IWorkRequestService _workRequestService;
        private readonly IMultimediaService _multimediaService;
        private readonly IStateChangeService _stateChangeService;
        private readonly IAuthHelperService _authHelperService;

        public WorkRequestController(IWorkRequestService workRequestService, IMultimediaService multimediaService,
            IStateChangeService stateChangeService, IAuthHelperService authHelperService)
        {
            _workRequestService = workRequestService;
            _multimediaService = multimediaService;
            _stateChangeService = stateChangeService;
            _authHelperService = authHelperService;
        }

        [HttpGet("all")]
        [Authorize(Roles ="CREW_MEMBER, DISPATCHER, WORKER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkRequestDto>))]
        public IActionResult GetAll()
        {
            return Ok(_workRequestService.GetAll());
        }


        [HttpGet]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WorkRequestDto>))]
        public IActionResult GetPaged([FromQuery] string searchParam, [FromQuery] WorkRequestField sortBy, [FromQuery] SortingDirection direction,
                                    [FromQuery][BindRequired] int page, [FromQuery][BindRequired] int perPage, [FromQuery] DocumentStatusFilter status,
                                    [FromQuery] OwnerFilter owner)
        {
            return Ok(_workRequestService.GetWorkRequestsPaged(sortBy, direction, page, perPage, status, owner, searchParam, User));
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            try
            {
                WorkRequestDto workRequest = _workRequestService.Get(id);
                return Ok(workRequest);
            }catch(WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }

        [HttpGet("statistics/{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestStatisticsDto))]
        public IActionResult GetWorkRequestStatisticsForUser(int userId)
        {
            if (_authHelperService.GetUserIDFromPrincipal(User) != userId)
                return Unauthorized("You can view statitics only for yourself");
            return Ok(_workRequestService.GetStatisticsForUser(userId));

        }

        [HttpGet("{id}/devices")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER, WORKER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DeviceDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetWorkRequestDevices(int id)
        {
            try
            {
                return Ok(_workRequestService.GetWorkRequestDevices(id));
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateWorkRequest([FromBody] WorkRequestDto workRequest)
        {
            try
            {
                WorkRequestDto newWorkRequest = _workRequestService.Insert(workRequest);
                return CreatedAtAction(nameof(GetById), new { id = newWorkRequest.ID}, newWorkRequest);
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (WorkRequestInvalidStateException wris)
            {
                return BadRequest(wris.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditWorkRequest(int id,[FromBody] WorkRequestDto workRequest)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), workRequest.ID))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                WorkRequestDto modified = _workRequestService.Update(workRequest);
                return Ok(modified);
            }
            catch (IncidentNotFoundException wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (UserNotFoundException unf)
            {
                return NotFound(unf.Message);
            }
            catch (WorkRequestInvalidStateException wris)
            {
                return BadRequest(wris.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteWorkRequest(int id)
        {

            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
                _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                _workRequestService.Delete(id);
                return NoContent();
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }

        [HttpPost("{id}/attachments")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> AttachFileAsync(int id, IFormFile file)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                await _multimediaService.AttachFileToWorkRequestAsync(file, id);
                return Ok();
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaAlreadyExists mae)
            {
                return BadRequest(mae.Message);
            }
            catch (MultimediaInfectedException mie)
            {
                return BadRequest(mie.Message);
            }
            catch (WorkRequestInvalidStateException wis)
            {
                return BadRequest(wis.Message);
            }
        }

        [HttpGet("{id}/attachments/{filename}")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(FileStreamResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFile(int id, string filename)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                return File(_multimediaService.GetWorkRequestAttachmentStream(id, filename), "application/octet-stream", filename);
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaNotFoundException mne)
            {
                return NotFound(mne.Message);
            }
        }


        [HttpGet("{id}/attachments")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MultimediaAttachmentDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult  GetWorkRequestAttachments(int id)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                return Ok(_multimediaService.GetWorkRequestAttachments(id));
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }


        [HttpGet("{id}/state-changes")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StateChangeHistoryDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetWorkRequestStateChanges(int id)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                return Ok(_stateChangeService.GetWorkRequestStateHistory(id));
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
        }


        [HttpDelete("{id}/attachments/{filename}")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAttachment(int id, string filename)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                _multimediaService.DeleteWorkRequestAttachment(id, filename);
                return Ok();
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (MultimediaNotFoundException mnf)
            {
                return NotFound(mnf.Message);
            }
            catch (WorkRequestInvalidStateException wis)
            {
                return BadRequest(wis.Message);
            }
            
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ApproveWorkRequest(int id)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                WorkRequestDto wr = _stateChangeService.ApproveWorkRequest(id, User);
                return Ok(wr);
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (WorkRequestInvalidStateException wnf)
            {
                return BadRequest(wnf.Message);
            }
        }

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CancelWorkRequest(int id)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                WorkRequestDto wr = _stateChangeService.CancelWorkRequest(id, User);
                return Ok(wr);
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (WorkRequestInvalidStateException wnf)
            {
                return BadRequest(wnf.Message);
            }
        }


        [HttpPut("{id}/deny")]
        [Authorize(Roles = "CREW_MEMBER, DISPATCHER", Policy = "ApprovedOnly")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkRequestDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DenyWorkRequest(int id)
        {
            try
            {
                if (User.IsInRole("CREW_MEMBER") &&
               _workRequestService.IsCrewMemberHandlingWorkRequest(_authHelperService.GetUserIDFromPrincipal(User), id))
                    return Unauthorized("Only crew members assigned to work request are allowed to edit it.");
                WorkRequestDto wr = _stateChangeService.DenyWorkRequest(id, User);
                return Ok(wr);
            }
            catch (WorkRequestNotFound wnf)
            {
                return NotFound(wnf.Message);
            }
            catch (WorkRequestInvalidStateException wnf)
            {
                return BadRequest(wnf.Message);
            }
        }

    }
}
