using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Call;
using SmartEnergy.Contract.CustomExceptions.Consumer;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class IncidentService : IIncidentService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly ITimeService _timeService;   
        private readonly IDeviceUsageService _deviceUsageService;
        private readonly ICallService _callService;
        private readonly IMapper _mapper;
        private readonly IAuthHelperService _authHelperService;
        private readonly IMailService _mailService;
        private readonly IConsumerService _consumerService;



        public IncidentService(SmartEnergyDbContext dbContext, ITimeService timeService, IDeviceUsageService deviceUsageService,  IMapper mapper, ICallService callService, IAuthHelperService authHelperService, IMailService mailService, IConsumerService consumerService)
        {
            _dbContext = dbContext;
            _timeService = timeService;
            _deviceUsageService = deviceUsageService;
            _callService = callService;
            _mapper = mapper;
            _authHelperService = authHelperService;
            _mailService = mailService;
            _consumerService = consumerService;


        }

           
        


        // determine what to delete with incident object
        public void Delete(int id)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.MultimediaAnchor)
                                                    .Include(x => x.NotificationAnchor)
                                                    .Include(x => x.IncidentDevices)
                                                    .Include(x => x.WorkRequest)
                                                    .FirstOrDefault(x => x.ID == id);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {id} dos not exist.");

            _dbContext.Incidents.Remove(incident);

           // Remove anchors
            _dbContext.MultimediaAnchors.Remove(incident.MultimediaAnchor);
            _dbContext.NotificationAnchors.Remove(incident.NotificationAnchor);
  

            _dbContext.SaveChanges();
        }

        public IncidentDto Get(int id)
        {
            Incident incident = _dbContext.Incidents.Find(id);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {id} does not exist.");

            return _mapper.Map<IncidentDto>(incident);
        }


        public List<IncidentDto> GetAll()
        {
            return _mapper.Map<List<IncidentDto>>(_dbContext.Incidents.ToList());
        }

        /// <summary>
        /// Get incident location
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>LocationDto</returns>
        public LocationDto GetIncidentLocation(int incidentId)
        {
            //TODO:
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                    .ThenInclude(p => p.Device)
                                                    .ThenInclude(o => o.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            DayPeriod currentDayPeriod = _timeService.GetCurrentDayPeriod();

            //Try getting location from devices
            List<DeviceUsage> devices = new List<DeviceUsage>();
            if (currentDayPeriod == DayPeriod.MORNING)
                devices = incident.IncidentDevices.OrderByDescending(x => x.Device.Location.MorningPriority).ToList();
            else if (currentDayPeriod == DayPeriod.NOON)
                devices = incident.IncidentDevices.OrderByDescending(x => x.Device.Location.NoonPriority).ToList();
            else
                devices = incident.IncidentDevices.OrderByDescending(x => x.Device.Location.NightPriority).ToList();

            foreach (DeviceUsage d in devices)
            {
                return _mapper.Map<LocationDto>(d.Device.Location);
            }

            incident = _dbContext.Incidents.Include(x => x.Calls)
                                                    .ThenInclude(p => p.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            //Try getting location from calls
            foreach (Call c in incident.Calls)
            {
                return _mapper.Map<LocationDto>(c.Location);
            }

            throw new LocationNotFoundException($"Location does not exist for incident with id {incidentId}");
        }

        public IncidentDto Insert(IncidentDto entity)
        {
            ValidateIncident(entity);

            MultimediaAnchor mAnchor = new MultimediaAnchor();
   
            NotificationAnchor nAnchor = new NotificationAnchor();

            Incident incident = _mapper.Map<Incident>(entity);
            incident.ID = 0;
            incident.MultimediaAnchor = mAnchor;
            incident.NotificationAnchor = nAnchor;
            incident.Timestamp = DateTime.Now;


            if (incident.ETR != null)
                incident.ETR = incident.ETR.Value.AddHours(2);






            incident.Priority = 0;
            incident.IncidentStatus = IncidentStatus.INITIAL; // with basic info only init

            

         

        
            _dbContext.Incidents.Add(incident);

            _dbContext.SaveChanges();

            return _mapper.Map<IncidentDto>(incident);
        }

        public IncidentDto Update(IncidentDto entity)
        {
            ValidateIncident(entity);

            Incident oldIncident = _dbContext.Incidents.Find(entity.ID);

            if (oldIncident == null)
                throw new IncidentNotFoundException($"Incident with id {entity.ID} does not exist");


            Incident entityIncident = _mapper.Map<Incident>(entity);

            if (entityIncident.Timestamp < oldIncident.Timestamp)
                throw new InvalidIncidentException("You have tried to modify outdated incident. Please, try again.");

            if (entityIncident.ETR != null)
                entityIncident.ETR = entityIncident.ETR.Value.AddHours(2);

            oldIncident.Update(_mapper.Map<Incident>(entityIncident));

            _dbContext.SaveChanges();
            
            return _mapper.Map<IncidentDto>(oldIncident);

        }



        private void ValidateIncident(IncidentDto entity)
        {

            if (!Enum.IsDefined(typeof(WorkType), entity.WorkType))
                throw new InvalidIncidentException("Undefined work type!");

            if (!Enum.IsDefined(typeof(IncidentStatus), entity.IncidentStatus))
                throw new InvalidIncidentException("Undefined incident status!");

            if (entity.Description == null || entity.Description.Length > 100)
                throw new InvalidIncidentException($"Description must be at most 100 characters long!");


            if (entity.VoltageLevel <= 0)
                throw new InvalidIncidentException("Voltage level have to be greater than 0!");

            //proveriti validacije za datume

            if (entity.IncidentDateTime > entity.ETA)
                throw new InvalidIncidentException($"ETA date cannot be before incident date!");

            if (entity.IncidentDateTime > entity.ATA)
                throw new InvalidIncidentException($"ATA date cannot be before incident date!");

         

            if (entity.WorkBeginDate < entity.IncidentDateTime)
                throw new InvalidIncidentException($"Sheduled date cannot be before incident date.");




        }
       
        
        
        /// <summary>
        /// Get priority for specific incident by finding device related to incident with highest priority.
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>Integer priority</returns>
        private int GetIncidentPriority(int incidentId)
        {

            int priority = -1;

            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                     .ThenInclude(p => p.Device)
                                                     .ThenInclude(o => o.Location)
                                                     .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");


            List<int> allPriorities = new List<int>();

            DayPeriod currentDayPeriod = _timeService.GetCurrentDayPeriod();

            



            //Try getting location from devices
            foreach (DeviceUsage d in incident.IncidentDevices)
            {
                if (currentDayPeriod == DayPeriod.MORNING)
                    allPriorities.Add(d.Device.Location.MorningPriority);
                else if(currentDayPeriod == DayPeriod.NOON)
                    allPriorities.Add(d.Device.Location.NoonPriority);
                else
                    allPriorities.Add(d.Device.Location.NightPriority);

            }


            if (allPriorities.Count != 0)
                priority = allPriorities.Max();
            else
                priority = 0;



            if (priority != -1)
                return priority;
            else
                return 0;

          

          
        }


        /// <summary>
        /// Connect specific crew with incident
        /// </summary>
        /// <param name="incidentId"></param>
        /// <param name="crewId"></param>
        /// <returns></returns>
        public IncidentDto AddCrewToIncident(int incidentId, int crewId)
        {
            Incident incident = _dbContext.Incidents.Find(incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id = {incidentId} does not exists!");


            Crew crew = _dbContext.Crews.Find(crewId);

            if (crew == null)
                throw new CrewNotFoundException($"Crew with id = {crewId} does not exists!");

            incident.CrewID = crew.ID;


            _dbContext.SaveChanges();

            return _mapper.Map<IncidentDto>(incident);







        }


        /// <summary>
        /// This function get all incidents which are ready for usage in work request
        /// </summary>
        /// <returns>List of ready incidents</returns>
        public List<IncidentDto> GetUnassignedIncidents()
        {

            bool isFree = true;

            List<Incident> allIncidents = _dbContext.Incidents.ToList();
            List<WorkRequest> allWorkRequests = _dbContext.WorkRequests.Include("Incident").ToList();

            List<Incident> unassignedIcidents = new List<Incident>();

            foreach(Incident incident in allIncidents)
            {
                isFree = true;

                foreach(WorkRequest workRequest in allWorkRequests)
                {
                    if (workRequest.IncidentID.Equals(incident.ID))
                    {
                        isFree = false;
                        break;
                    }
                   
                }

                if( !incident.IncidentStatus.Equals(IncidentStatus.INITIAL) && isFree)
                    unassignedIcidents.Add(incident);


            }


            
            return _mapper.Map<List<IncidentDto>>(unassignedIcidents);
        }

        public void AddDeviceToIncident(int incidentId, int deviceId)
        {

            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                    .ThenInclude(p => p.Device)
                                                    .ThenInclude(o => o.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            Device device = _dbContext.Devices.Include(x => x.Location).FirstOrDefault( x => x.ID == deviceId);

            if (device == null)
                throw new DeviceNotFoundException($"Device with id = { deviceId} does not exists!");

            // ako je vec dodat device u incident   
            // proverim da li se adresa novog poklapa (city, street, zip)
            if (incident.IncidentDevices.Count != 0)                                                    
            {                                        
                foreach(DeviceUsage du in incident.IncidentDevices)
                {
                    if (!CompareLocation(du.Device.Location, device.Location))
                        throw new InvalidDeviceException($"Device has to be on {du.Device.Location.Street}, {du.Device.Location.City}, {du.Device.Location.Zip}!");
                }
            }

            List<Call> callWithoutIncident = _dbContext.Calls.Include("Location").Where(x => x.IncidentID == null).ToList();
            
            foreach(Call c in callWithoutIncident)
            {
                if(CompareLocation(c.Location, device.Location))
                {
                    c.IncidentID = incidentId;
                    _callService.Update(_mapper.Map<CallDto>(c));
                }

            }



            if (incident.IncidentDevices.Find(x => x.DeviceID == deviceId) != null)
                throw new InvalidDeviceUsageException($"Device with id = {deviceId} is already added to incident!");



            _deviceUsageService.Insert(new DeviceUsageDto { IncidentID = incidentId, DeviceID = deviceId });

            incident.Priority = GetIncidentPriority(incident.ID);
            
            if(incident.IncidentStatus == IncidentStatus.INITIAL)
                incident.IncidentStatus = IncidentStatus.UNRESOLVED;


            _dbContext.SaveChanges();






        }

        public IncidentDto RemoveCrewFromIncidet(int incidentId)
        {
            Incident incident = _dbContext.Incidents.Find(incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id = {incidentId} does not exists!");


            

            incident.CrewID = null;
            _dbContext.SaveChanges();


            return _mapper.Map<IncidentDto>(incident);
        }

        public void RemoveDeviceFromIncindet(int incidentId, int deviceId)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                   .ThenInclude(p => p.Device)
                                                   .ThenInclude(o => o.Location)
                                                   .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            Device device = _dbContext.Devices.Find(deviceId);

            if (device == null)
                throw new DeviceNotFoundException($"Device with id = { deviceId} does not exists!");


            DeviceUsage toRemove = incident.IncidentDevices.Find(x => x.DeviceID == deviceId);

            if ( toRemove == null)
                throw new InvalidDeviceUsageException($"Device with id = {deviceId} is not connected with incident with id = {incidentId}");


            
            _deviceUsageService.Delete(toRemove.ID);
            incident.Priority = GetIncidentPriority(incident.ID);


            _dbContext.SaveChanges();
        }

        public List<IncidentMapDisplayDto> GetUnresolvedIncidentsForMap()
        {
            List<Incident> incidents = _dbContext.Incidents.Include(x => x.Crew)
                                                           .Where(x => x.IncidentStatus == IncidentStatus.UNRESOLVED).ToList();
            List<IncidentMapDisplayDto> returnValue = new List<IncidentMapDisplayDto>();

            foreach(Incident incident in incidents)
            {
                try
                {
                    returnValue.Add(new IncidentMapDisplayDto()
                    {
                        ID = incident.ID,
                        IncidentDateTime = incident.IncidentDateTime,
                        Priority = incident.Priority,
                        Location = GetIncidentLocation(incident.ID),
                        Crew = _mapper.Map<CrewDto>(incident.Crew)
                    }) ;
                }catch
                {

                }
            }

            return returnValue;
        }


     /// <summary>
     /// Get all calls for incident
     /// Compare location of devices assigned to incident and calls location
     /// </summary>
     /// <param name="incidentId"></param>
     /// <returns></returns>
        public List<CallDto> GetIncidentCalls(int incidentId)
        {

            List<DeviceDto> incidentDevices = GetIncidentDevices(incidentId);
            List<CallDto> allCalls = _callService.GetAll();

           

            if(incidentDevices.Count != 0)
            {

                foreach(CallDto c in allCalls)
                {
                    if(c.IncidentID == null && CompareLocation(_mapper.Map<Location>(c.Location), _mapper.Map<Location>( incidentDevices[0].Location)))
                    {
                        c.IncidentID = incidentId;
                        _callService.Update(_mapper.Map<CallDto>(c));
                    }


                }

              
            }

            return _callService.GetAll().FindAll(x => x.IncidentID == incidentId).ToList();


        }

        public int GetNumberOfCalls(int incidentId)
        {
            return _callService.GetAll().Where(x => x.IncidentID == incidentId).Count();
        }

        public int GetNumberOfAffectedConsumers(int incidentId)
        {
            int affectedConsumers = 0;

            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                   .ThenInclude(p => p.Device)
                                                   .ThenInclude(o => o.Location)
                                                   .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            List<string> deviceStreets = new List<string>();

            foreach (DeviceUsage device in incident.IncidentDevices)
            {
                if (!deviceStreets.Contains(device.Device.Location.Street.ToLower().Trim()))
                    deviceStreets.Add(device.Device.Location.Street.ToLower().Trim());

            }

            List<Consumer> consumers = _dbContext.Consumers.Include("Location").ToList();

            foreach(string deviceStreet in deviceStreets)
            {
                foreach(Consumer consumer in consumers)
                {
                    if (consumer.Location.Street.ToLower().Trim().Equals(deviceStreet))
                        affectedConsumers++;
                }
            }

           



            return affectedConsumers;
            
        }

        public List<DeviceDto> GetIncidentDevices(int incidentId)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                   .ThenInclude(p => p.Device)
                                                   .ThenInclude(o => o.Location)
                                                   .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            List<Device> incidentDevices = new List<Device>();

            foreach (DeviceUsage deviceUsage in incident.IncidentDevices)
                incidentDevices.Add(deviceUsage.Device);

            return _mapper.Map<List<DeviceDto>>(incidentDevices);

        }

        public void SetIncidentPriority(int incidentId)
        {
            int incidentPriority = GetIncidentPriority(incidentId);

            Incident incident = _dbContext.Incidents.Find(incidentId);

            incident.Priority = incidentPriority;
            _dbContext.SaveChanges();

        }

        public List<DeviceDto> GetUnrelatedDevices(int incidentId)
        {

            
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                  .ThenInclude(p => p.Device)
                                                  .ThenInclude(o => o.Location)
                                                  .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");


            List<int> incidentDeviceIds = new List<int>();

            


            foreach(DeviceUsage deviceUsage in _dbContext.DeviceUsages.ToList())
            {
                if (deviceUsage.IncidentID == incidentId)
                    incidentDeviceIds.Add(deviceUsage.DeviceID);
            }

            bool condition = true;
            List<Device> devicesToReturn = new List<Device>();

            List<Device> allDevices = _dbContext.Devices.Include("Location").ToList();

            foreach(Device d in allDevices)
            {
                condition = true;
                foreach(int deviceId in incidentDeviceIds)
                {
                    if(d.ID == deviceId)
                    {
                        condition = false;
                        break;
 
                    }

                }

                if(condition)
                    devicesToReturn.Add(d);
            }

           

            return _mapper.Map<List<DeviceDto>>(devicesToReturn);
        }

        public CrewDto GetIncidentCrew(int incidentId)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.Crew)
                                                    .ThenInclude(x => x.CrewMembers)
                                                    .FirstOrDefault(x => x.ID == incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            return _mapper.Map<CrewDto>(incident.Crew);
        }

        private bool CompareLocation(Location location1, Location location2)
        {
            if( (location1.Zip == location2.Zip) &&
                (location1.Street.Equals(location2.Street))
                && (location1.City.Equals(location2.City)))
            {
                return true;
            }else
            {
                return false;
            }
                    
        }

        public CallDto AddIncidentCall(int incidentId, CallDto newCall)
        {

            Incident incident = _dbContext.Incidents.Include(x => x.Crew)
                                                  .ThenInclude(x => x.CrewMembers)
                                                  .FirstOrDefault(x => x.ID == incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exists.");


            ValidateCall(newCall);

            newCall.IncidentID = incidentId;
            return _callService.Insert(newCall);
        }


        private void ValidateCall(CallDto entity)
        {
            if (entity.Hazard.Trim().Equals("") || entity.Hazard == null)
                throw new InvalidCallException("You have to enter call hazard!");

            if (!Enum.IsDefined(typeof(CallReason), entity.CallReason))
                throw new InvalidCallException("Undefined call reason!");


            if (_dbContext.Location.Any(x => x.ID == entity.LocationID) == false)
                throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");


            if (entity.ConsumerID != 0 && entity.ConsumerID != null)  // ako nije anoniman
            {

                if (_dbContext.Consumers.Any(x => x.ID == entity.ConsumerID) == false)
                    throw new ConsumerNotFoundException($"Consumer with id = {entity.ConsumerID} does not exists!");

            }
        }

        public void AssignIncidetToUser(int incidentId, int userId)
        {
            //Incident incident = _dbContext.Incidents.Find(incidentId);

            //if (incident == null)
            //    throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");


            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                 .ThenInclude(p => p.Device)
                                                 .ThenInclude(o => o.Location)
                                                 .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

          

          


            User user = _dbContext.Users.Find(userId);
            if (user == null)
                throw new UserNotFoundException($"User with id {userId} does not exist.");

            incident.UserID = userId;


            int locationId = -1;

            if(incident.IncidentDevices.Count != 0)
            {
                locationId = incident.IncidentDevices[0].Device.LocationID;
            }

            List<Consumer> consumers = _dbContext.Consumers.Include(x => x.User).Where(x => x.LocationID == locationId).ToList();

            foreach(Consumer c in consumers)
            {
                _mailService.SendMail(c.User.Email, "Solving incident", "Problem will be solved as soon as possible. We are working!");
            }


           

          




            _dbContext.SaveChanges();




        }

        public IncidentListDto GetIncidentsPaged(IncidentFields sortBy, SortingDirection direction, int page, int perPage, IncidentFilter filter, OwnerFilter owner, string searchParam, ClaimsPrincipal user)
        {
            IQueryable<Incident> incidents = _dbContext.Incidents.Include(x => x.User).AsQueryable();

            incidents = FilterIncidents(incidents, filter);
            incidents = FilterIncidentsByOwner(incidents, owner, user);
            incidents = SearchIncidents(incidents, searchParam);
            incidents = SortIncidents(incidents, sortBy, direction);

            int resourceCount = incidents.Count();
            incidents = incidents.Skip(page * perPage)
                                    .Take(perPage);

            IncidentListDto returnValue = new IncidentListDto()
            {
                Incidents = _mapper.Map<List<IncidentDto>>(incidents.ToList()),
                TotalCount = resourceCount
            };

            return returnValue;
        }


        private IQueryable<Incident> FilterIncidents(IQueryable<Incident> incidents, IncidentFilter filter)
        {
            //Filter by status, ignore if ALL
            switch (filter)
            {
                case IncidentFilter.CONFIRMED:
                    return incidents.Where(x => x.Confirmed == true);
                case IncidentFilter.RESOLVED:
                    return incidents.Where(x => x.IncidentStatus == IncidentStatus.RESOLVED);
                case IncidentFilter.UNRESOLVED:
                    return incidents.Where(x => x.IncidentStatus == IncidentStatus.UNRESOLVED);
                case IncidentFilter.INITIAL:
                    return incidents.Where(x => x.IncidentStatus == IncidentStatus.INITIAL);
                case IncidentFilter.PLANNED:
                    return incidents.Where(x => x.WorkType == WorkType.PLANNED);
                case IncidentFilter.UNPLANNED:
                    return incidents.Where(x => x.WorkType == WorkType.UNPLANNED);

              
            }

            return incidents;
        }

        private IQueryable<Incident> FilterIncidentsByOwner(IQueryable<Incident> incidents, OwnerFilter owner, ClaimsPrincipal user)
        {
            int userId = _authHelperService.GetUserIDFromPrincipal(user);
            if (owner == OwnerFilter.mine)
                incidents = incidents.Where(x => x.User.ID == userId);

            return incidents;
        }

        private IQueryable<Incident> SearchIncidents(IQueryable<Incident> incidents, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam)) //Ignore empty search
                return incidents;
           
            return incidents.Where(x => x.ID.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                                        x.VoltageLevel.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                                        x.Priority.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()));


        }

        private IQueryable<Incident> SortIncidents(IQueryable<Incident> incidents, IncidentFields sortBy, SortingDirection direction)
        {
            //Sort
            if (direction == SortingDirection.asc)
            {
                switch (sortBy)
                {
                    case IncidentFields.ID:
                        return incidents.OrderBy(x => x.ID);
                    case IncidentFields.ATA:
                        return incidents.OrderBy(x => x.ATA);
                    case IncidentFields.CONFIRMED:
                        return incidents.OrderBy(x => x.Confirmed);
                    case IncidentFields.ETA:
                        return incidents.OrderBy(x => x.ETA);
                    case IncidentFields.ETR:
                        return incidents.OrderBy(x => x.ETR.Value.Hour)
                                         .ThenBy(x => x.ETR.Value.Minute);

                    case IncidentFields.INCIDENTDATETIME:
                        return incidents.OrderBy(x => x.IncidentDateTime);
                    case IncidentFields.PLANNEDWORK:
                        return incidents.OrderBy(x => x.WorkBeginDate);
                    case IncidentFields.PRIORITY:
                        return incidents.OrderBy(x => x.Priority);
                    case IncidentFields.STATUS:
                        return incidents.OrderBy(x => x.IncidentStatus);
                    case IncidentFields.TYPE:
                        return incidents.OrderBy(x => x.WorkType);
                    case IncidentFields.VOLTAGELEVEL:
                        return incidents.OrderBy(x => x.VoltageLevel);

                }

            }
            else
            {
                switch (sortBy)
                {
                    case IncidentFields.ID:
                        return incidents.OrderByDescending(x => x.ID);
                    case IncidentFields.ATA:
                        return incidents.OrderByDescending(x => x.ATA);
                    case IncidentFields.CONFIRMED:
                        return incidents.OrderByDescending(x => x.Confirmed);
                    case IncidentFields.ETA:
                        return incidents.OrderByDescending(x => x.ETA);
                    case IncidentFields.ETR:
                        return incidents.OrderByDescending(x => x.ETR.Value.Hour)
                                         .ThenBy(x => x.ETR.Value.Minute);
                    case IncidentFields.INCIDENTDATETIME:
                        return incidents.OrderByDescending(x => x.IncidentDateTime);
                    case IncidentFields.PLANNEDWORK:
                        return incidents.OrderByDescending(x => x.WorkBeginDate);
                    case IncidentFields.PRIORITY:
                        return incidents.OrderByDescending(x => x.Priority);
                    case IncidentFields.STATUS:
                        return incidents.OrderByDescending(x => x.IncidentStatus);
                    case IncidentFields.TYPE:
                        return incidents.OrderByDescending(x => x.WorkType);
                    case IncidentFields.VOLTAGELEVEL:
                        return incidents.OrderByDescending(x => x.VoltageLevel);
            }

            }

            return incidents;
        }

        public IncidentStatisticsDto GetStatisticsForUser(int userId)
        {
            IQueryable<Incident> incidents = _dbContext.Incidents.Where(x => x.UserID == userId);

            IncidentStatisticsDto statistics = new IncidentStatisticsDto()
            {
                Total = incidents.Count(),
                Planned = incidents.Where(x => x.WorkType == WorkType.PLANNED).Count(),
                Unplanned = incidents.Where(x => x.WorkType == WorkType.UNPLANNED).Count(),
                Resolved = incidents.Where(x => x.IncidentStatus == IncidentStatus.RESOLVED).Count(),
                Unresolved = incidents.Where(x => x.IncidentStatus == IncidentStatus.UNRESOLVED).Count(),
            };

            return statistics;

        }
    }
}
