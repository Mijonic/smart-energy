using AutoMapper;
using Dapr.Client;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.MicroserviceAPI.DomainModels;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class WorkRequestService : IWorkRequestService
    {
        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IIncidentService _incidentService;
        private readonly IDeviceUsageService _deviceUsageService;
        private readonly IAuthHelperService _authHelperService;
        private readonly IMultimediaService _multimediaService;
        private readonly DaprClient _daprClient;

        public WorkRequestService(MicroserviceDbContext dbContext, IMapper mapper,
            IIncidentService incidentService, IDeviceUsageService deviceUsageService,
            IAuthHelperService authHelperService, IMultimediaService multimedia, DaprClient daprClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _incidentService = incidentService;
            _deviceUsageService = deviceUsageService;
            _authHelperService = authHelperService;
            _multimediaService = multimedia;
            _daprClient = daprClient;
        }

        public void Delete(int id)
        {
            WorkRequest wr = _dbContext.WorkRequests.Include( x => x.WorkPlan)
                                                    .Include(x => x.WorkPlan.MultimediaAnchor)
                                                    .Include(x => x.WorkPlan.StateChangeAnchor)
                                                    .Include(x => x.WorkPlan.NotificationAnchor)
                                                    .Include(x => x.MultimediaAnchor)
                                                    .ThenInclude( x => x.MultimediaAttachments)
                                                    .Include(x => x.NotificationsAnchor)
                                                    .Include(x => x.StateChangeAnchor)
                                                    .Include( x => x.DeviceUsage)
                                                    .FirstOrDefault( x=> x.ID == id);
            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {id} does not exist.");
            _dbContext.WorkRequests.Remove(wr);
            List<string> files = new List<string>();
            foreach (MultimediaAttachment att in wr.MultimediaAnchor.MultimediaAttachments)
                files.Add(att.Url);

            //Remove anchors
            _dbContext.MultimediaAnchors.Remove(wr.MultimediaAnchor);
            _dbContext.NotificationAnchors.Remove(wr.NotificationsAnchor);
            _dbContext.StateChangeAnchors.Remove(wr.StateChangeAnchor);
            //Remove work plan anchors
            _dbContext.MultimediaAnchors.Remove(wr.WorkPlan.MultimediaAnchor);
            _dbContext.NotificationAnchors.Remove(wr.WorkPlan.NotificationAnchor);
            _dbContext.StateChangeAnchors.Remove(wr.WorkPlan.StateChangeAnchor);

            _dbContext.SaveChanges();

            foreach (string file in files)
                _multimediaService.DeleteWorkRequestFileOnDisk(id, file);
        }

        public WorkRequestDto Get(int id)
        {
            WorkRequest workRequest = _dbContext.WorkRequests.Find(id);

            if (workRequest == null)
                throw new WorkRequestNotFound($"Work request with id {id} does not exist.");

            return _mapper.Map<WorkRequestDto>(workRequest);
        }

        public List<WorkRequestDto> GetAll()
        {
            return _mapper.Map<List<WorkRequestDto>>(_dbContext.WorkRequests.ToList());
        }


        //SREDITI OVO
        public async Task<List<DeviceDto>> GetWorkRequestDevices(int workRequestId)
        {
            //WorkRequest workRequest = _dbContext.WorkRequests.Include(x => x.DeviceUsage)
            //                                                 .ThenInclude(x => x.Device)
            //                                                 .ThenInclude(x => x.Location)
            //                                                 .FirstOrDefault(x => x.ID == workRequestId);

            WorkRequest workRequest = _dbContext.WorkRequests.Include(x => x.DeviceUsage).FirstOrDefault(x => x.ID == workRequestId);

            if (workRequest == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");


            List<DeviceDto> workRequestDevices = new List<DeviceDto>();
            DeviceDto deviceDto = new DeviceDto();


            foreach (DeviceUsage deviceUsage in workRequest.DeviceUsage)
            {
                try
                {
                    deviceDto = await _daprClient.InvokeMethodAsync<DeviceDto>(HttpMethod.Get, "smartenergydevice", $"/api/devices/{deviceUsage.DeviceID}");

                }
                catch (Exception e)
                {
                    throw new DeviceNotFoundException("Device service is unavailable right now.");
                }

                //try
                //{
                //    LocationDto location = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{deviceDto.LocationID}");
                //    deviceDto.Location = location;

                //}
                //catch (Exception e)
                //{
                //    throw new LocationNotFoundException("Location service is unavailable right now.");
                //}

                workRequestDevices.Add(deviceDto);



            }


            return workRequestDevices;



        }

        public WorkRequestsListDto GetWorkRequestsPaged(WorkRequestField sortBy, SortingDirection direction, int page, int perPage,
            DocumentStatusFilter status, OwnerFilter owner, string searchParam, ClaimsPrincipal user)
        {
            IQueryable<WorkRequest> wrPaged = _dbContext.WorkRequests.Include(x => x.User).AsQueryable();

            wrPaged = FilterWorkRequestsByStatus(wrPaged, status);
            wrPaged = FilterWorkRequestsByOwner(wrPaged, owner, user);
            wrPaged = SearchWorkRequests(wrPaged, searchParam);
            wrPaged = SortWorkRequests(wrPaged, sortBy, direction);

            int resourceCount = wrPaged.Count();
            wrPaged = wrPaged.Skip(page * perPage)
                                    .Take(perPage);

            WorkRequestsListDto returnValue = new WorkRequestsListDto()
            {
                WorkRequests = _mapper.Map<List<WorkRequestDto>>(wrPaged.ToList()),
                TotalCount = resourceCount
            };

            return returnValue;
        }

        public WorkRequestDto Insert(WorkRequestDto entity)
        {
            ValidateWorkRequest(entity);

            MultimediaAnchor mAnchor = new MultimediaAnchor();

            StateChangeAnchor sAnchor = new StateChangeAnchor();

            NotificationAnchor nAnchor = new NotificationAnchor();

            StateChangeHistory stateChange = new StateChangeHistory()
            {
                DocumentStatus = DocumentStatus.DRAFT,
                UserID = entity.UserID,
                
            };

            sAnchor.StateChangeHistories = new List<StateChangeHistory>() { stateChange };

            WorkRequest workRequest = _mapper.Map<WorkRequest>(entity);
            workRequest.ID = 0;
            workRequest.MultimediaAnchor = mAnchor;
            workRequest.NotificationsAnchor = nAnchor;
            workRequest.StateChangeAnchor = sAnchor;
            workRequest.DocumentStatus = DocumentStatus.DRAFT;
            _dbContext.WorkRequests.Add(workRequest);

            _dbContext.SaveChanges();

            _deviceUsageService.CopyIncidentDevicesToWorkRequest(workRequest.IncidentID, workRequest.ID);

            return _mapper.Map<WorkRequestDto>(workRequest);
        }

        public WorkRequestDto Update(WorkRequestDto entity)
        {
            ValidateWorkRequest(entity);
            WorkRequest existing = _dbContext.WorkRequests.Find(entity.ID);
            if (existing == null)
                throw new WorkRequestNotFound($"Work request with id {entity.ID} does not exist");

            existing.Update(_mapper.Map<WorkRequest>(entity));

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(existing);

            
        }

        private async void ValidateWorkRequest(WorkRequestDto entity)
        {
            if (_dbContext.Incidents.Find(entity.IncidentID) == null)
            {
                throw new IncidentNotFoundException($"Attached incident with id {entity.IncidentID} does not exist.");
            }

            if(_dbContext.Users.Find(entity.UserID) == null)
                throw new UserNotFoundException($"Attached user with id {entity.UserID} does not exist.");

            WorkRequest wr = _dbContext.WorkRequests.FirstOrDefault(x => x.IncidentID == entity.IncidentID);
            if (wr != null && wr.ID != entity.ID )
                throw new WorkRequestInvalidStateException($"Work request already created for incident with id {entity.IncidentID}");

            if (wr != null && (wr.DocumentStatus == DocumentStatus.APPROVED || wr.DocumentStatus == DocumentStatus.CANCELLED))
                throw new WorkRequestInvalidStateException($"Work request is in {wr.DocumentStatus.ToString()} state and cannot be edited.");

            if (entity.StartDate.CompareTo(entity.EndDate) > 0)
                throw new WorkRequestInvalidStateException($"Start date cannot be after end date.");

            if (entity.StartDate.CompareTo(DateTime.Now) < 0)
                throw new WorkRequestInvalidStateException($"Start date cannot be in the past.");

            if (entity.Purpose == null || entity.Purpose.Length > 100)
                throw new WorkRequestInvalidStateException($"Purpose must be at most 100 characters long and is required.");

            if (entity.Note != null && entity.Note.Length > 100)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.Details != null && entity.Details.Length > 100)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.CompanyName != null && entity.CompanyName.Length > 50)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.Phone != null && entity.Phone.Length > 30)
                throw new WorkRequestInvalidStateException($"Phone must be at most 30 characters long.");

            if (entity.Street != null && entity.Street.Length > 50)
                throw new WorkRequestInvalidStateException($"Street must be at most 50 characters long.");

            if (entity.Street == null || entity.Street != "")
            {
                try
                {
                    LocationDto location = await _incidentService.GetIncidentLocation(entity.IncidentID);
                    entity.Street = location.Street + ", " + location.City;
                }
                catch {
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }
            }
        }


        private IQueryable<WorkRequest> FilterWorkRequestsByStatus(IQueryable<WorkRequest> wr, DocumentStatusFilter status)
        {
            //Filter by status, ignore if ALL
            switch (status)
            {
                case DocumentStatusFilter.approved:
                    return wr.Where(x => x.DocumentStatus == DocumentStatus.APPROVED);
                case DocumentStatusFilter.canceled:
                    return wr.Where(x => x.DocumentStatus == DocumentStatus.CANCELLED);
                case DocumentStatusFilter.denied:
                    return wr.Where(x => x.DocumentStatus == DocumentStatus.DENIED);
                case DocumentStatusFilter.draft:
                    return wr.Where(x => x.DocumentStatus == DocumentStatus.DRAFT);
            }

            return wr;
        }

        private IQueryable<WorkRequest> FilterWorkRequestsByOwner(IQueryable<WorkRequest> wr, OwnerFilter owner, ClaimsPrincipal user)
        {
            int userId = _authHelperService.GetUserIDFromPrincipal(user);
            if (owner == OwnerFilter.mine)
                wr = wr.Where(x => x.User.ID == userId);
            return wr;
        }


        private IQueryable<WorkRequest> SearchWorkRequests(IQueryable<WorkRequest> wr, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam)) //Ignore empty search
                return wr;
            ///Perform search
            return wr.Where(x => x.Street.Contains(searchParam) ||
                                               x.StartDate.ToString().Contains(searchParam) ||
                                               x.EndDate.ToString().Contains(searchParam) ||
                                               x.User.Username.Contains(searchParam) ||
                                               x.CompanyName.Contains(searchParam) ||
                                               x.Phone.Contains(searchParam) ||
                                               x.CreatedOn.ToString().Contains(searchParam));
        }

        private IQueryable<WorkRequest> SortWorkRequests(IQueryable<WorkRequest> wr, WorkRequestField sortBy, SortingDirection direction)
        {
            //Sort
            if (direction == SortingDirection.asc)
            {
                switch (sortBy)
                {
                    case WorkRequestField.id:
                        return wr.OrderBy(x => x.ID);
                    case WorkRequestField.company:
                        return wr.OrderBy(x => x.CompanyName);
                    case WorkRequestField.createdby:
                        return wr.OrderBy(x => x.User.Username);
                    case WorkRequestField.creationdate:
                        return wr.OrderBy(x => x.CreatedOn);
                    case WorkRequestField.emergency:
                        return wr.OrderBy(x => x.IsEmergency);
                    case WorkRequestField.enddate:
                        return wr.OrderBy(x => x.EndDate);
                    case WorkRequestField.incident:
                        return wr.OrderBy(x => x.IncidentID);
                    case WorkRequestField.phoneno:
                        return wr.OrderBy(x => x.Phone);
                    case WorkRequestField.startdate:
                        return wr.OrderBy(x => x.StartDate);
                    case WorkRequestField.status:
                        return wr.OrderBy(x => x.DocumentStatus);
                    case WorkRequestField.street:
                        return wr.OrderBy(x => x.Street);
                    case WorkRequestField.type:
                        return wr.OrderBy(x => x.DocumentType);
                }

            }
            else
            {
                switch (sortBy)
                {
                    case WorkRequestField.id:
                        return wr.OrderByDescending(x => x.ID);
                    case WorkRequestField.company:
                        return wr.OrderByDescending(x => x.CompanyName);
                    case WorkRequestField.createdby:
                        return wr.OrderByDescending(x => x.User.Username);
                    case WorkRequestField.creationdate:
                        return wr.OrderByDescending(x => x.CreatedOn);
                    case WorkRequestField.emergency:
                        return wr.OrderByDescending(x => x.IsEmergency);
                    case WorkRequestField.enddate:
                        return wr.OrderByDescending(x => x.EndDate);
                    case WorkRequestField.incident:
                        return wr.OrderByDescending(x => x.IncidentID);
                    case WorkRequestField.phoneno:
                        return wr.OrderByDescending(x => x.Phone);
                    case WorkRequestField.startdate:
                        return wr.OrderByDescending(x => x.StartDate);
                    case WorkRequestField.status:
                        return wr.OrderByDescending(x => x.DocumentStatus);
                    case WorkRequestField.street:
                        return wr.OrderByDescending(x => x.Street);
                    case WorkRequestField.type:
                        return wr.OrderByDescending(x => x.DocumentType);
                }

            }

            return wr;
        }

        public WorkRequestStatisticsDto GetStatisticsForUser(int userId)
        {
            IQueryable<WorkRequest> workRequests = _dbContext.WorkRequests.Where(x => x.UserID == userId);

            WorkRequestStatisticsDto statistics = new WorkRequestStatisticsDto()
            {
                Total = workRequests.Count(),
                Approved = workRequests.Where(x => x.DocumentStatus == DocumentStatus.APPROVED).Count(),
                Denied = workRequests.Where(x => x.DocumentStatus == DocumentStatus.DENIED).Count(),
                Cancelled= workRequests.Where(x => x.DocumentStatus == DocumentStatus.CANCELLED).Count(),
                Draft = workRequests.Where(x => x.DocumentStatus == DocumentStatus.DRAFT).Count(),
            };

            return statistics;

        }

        public bool IsCrewMemberHandlingWorkRequest(int crewMemberId, int workRequestId)
        {
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.Incident)
                                                    .ThenInclude(x => x.Crew)
                                                    .ThenInclude(x => x.CrewMembers)
                                                    .FirstOrDefault(x => x.ID == workRequestId);
            if(wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist");

            if (wr.Incident.Crew.CrewMembers.FirstOrDefault(x => x.ID == crewMemberId) == null)
                return false;

            return true;

        }
    }
}
