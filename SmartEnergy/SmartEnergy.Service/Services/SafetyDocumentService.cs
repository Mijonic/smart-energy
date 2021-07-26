using AutoMapper;
using SmartEnergy.Contract.CustomExceptions.SafetyDocument;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.CustomExceptions.WorkPlan;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SmartEnergy.Service.Services
{
   

    public class SafetyDocumentService : ISafetyDocumentService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDeviceUsageService _deviceUsageService;
        private readonly IAuthHelperService _authHelperService;


        public SafetyDocumentService(SmartEnergyDbContext dbContext, IMapper mapper, IDeviceUsageService deviceUsageService, IAuthHelperService authHelperService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _deviceUsageService = deviceUsageService;
            _authHelperService = authHelperService;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public SafetyDocumentDto Get(int id)
        {
            SafetyDocument safetyDocument = _dbContext.SafetyDocuments.Include(x => x.User).FirstOrDefault(x => x.ID.Equals(id));

            if (safetyDocument == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {id} does not exist.");


            SafetyDocumentDto sf = _mapper.Map<SafetyDocumentDto>(safetyDocument);

            CrewDto crew = GetCrewForSafetyDocument(sf.ID);

            if (crew != null)
                sf.CrewName = crew.CrewName;
            else
                sf.CrewName = "/";

            return sf;
        }

        public List<SafetyDocumentDto> GetAll()
        {
            List<SafetyDocumentDto> allSafetDocuments = _mapper.Map<List<SafetyDocumentDto>>(_dbContext.SafetyDocuments.Include(x => x.User).ToList());

            foreach(SafetyDocumentDto sf in allSafetDocuments)
            {
                CrewDto crew = GetCrewForSafetyDocument(sf.ID);

                if (crew != null)
                    sf.CrewName = crew.CrewName;
                else
                    sf.CrewName = "/";
               

                
            }

            return allSafetDocuments;
        }

        public List<SafetyDocumentDto> GetAllMineSafetyDocuments(OwnerFilter owner, ClaimsPrincipal user)
        {

            List<SafetyDocumentDto> allSafetDocuments = _mapper.Map<List<SafetyDocumentDto>>(_dbContext.SafetyDocuments.Include(x => x.User).ToList());

            foreach (SafetyDocumentDto sf in allSafetDocuments)
            {
                CrewDto crew = GetCrewForSafetyDocument(sf.ID);

                if (crew != null)
                    sf.CrewName = crew.CrewName;
                else
                    sf.CrewName = "/";



            }

          

            int userId = _authHelperService.GetUserIDFromPrincipal(user);
            if (owner == OwnerFilter.mine)
                allSafetDocuments = allSafetDocuments.Where(x => x.User.ID == userId).ToList();

            return allSafetDocuments;
        }

        public CrewDto GetCrewForSafetyDocument(int safetyDocumentId)
        {
            SafetyDocument sf = _dbContext.SafetyDocuments.Include(x => x.WorkPlan)
                                                   .ThenInclude(x => x.WorkRequest)
                                                   .ThenInclude(x => x.Incident)
                                                   .ThenInclude(x => x.Crew)
                                                   .FirstOrDefault(x => x.ID == safetyDocumentId);

            if (sf == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {safetyDocumentId} does not exist.");

            return _mapper.Map<CrewDto>(sf.WorkPlan.WorkRequest.Incident.Crew);
        }

        public List<DeviceDto> GetSafetyDocumentDevices(int safetyDocumentId)
        {
            SafetyDocument sf = _dbContext.SafetyDocuments.Include(x => x.DeviceUsages)
                                                             .ThenInclude(x => x.Device)
                                                             .ThenInclude(x => x.Location)
                                                             .FirstOrDefault(x => x.ID == safetyDocumentId);
            if (sf == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {safetyDocumentId} does not exist.");

            List<Device> devices = new List<Device>();
            foreach (DeviceUsage d in sf.DeviceUsages)
                devices.Add(d.Device);

            return _mapper.Map<List<DeviceDto>>(devices);
        }

        public SafetyDocumentDto Insert(SafetyDocumentDto entity)
        {

            ValidateSafetyDocument(entity);

            MultimediaAnchor mAnchor = new MultimediaAnchor();

            StateChangeAnchor sAnchor = new StateChangeAnchor();

            NotificationAnchor nAnchor = new NotificationAnchor();

            StateChangeHistory stateChange = new StateChangeHistory()
            {
                DocumentStatus = DocumentStatus.DRAFT,
                UserID = entity.UserID,

            };

            entity.User = null;

            sAnchor.StateChangeHistories = new List<StateChangeHistory>() { stateChange };

            SafetyDocument safetyDocument = _mapper.Map<SafetyDocument>(entity);
            safetyDocument.ID = 0;
            safetyDocument.User = null;
            safetyDocument.MultimediaAnchor = mAnchor;        
            safetyDocument.StateChangeAnchor = sAnchor;
            safetyDocument.DocumentStatus = DocumentStatus.DRAFT;
            safetyDocument.NotificationAnchor = nAnchor;

            _dbContext.SafetyDocuments.Add(safetyDocument);

            _dbContext.SaveChanges();

            _deviceUsageService.CopyIncidentDevicesToSafetyDocument(safetyDocument.WorkPlanID, safetyDocument.ID);

            return _mapper.Map<SafetyDocumentDto>(safetyDocument);
        }

        public SafetyDocumentDto Update(SafetyDocumentDto entity)
        {
            ValidateSafetyDocument(entity);

            SafetyDocument existing = _dbContext.SafetyDocuments.Find(entity.ID);

            if (existing == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {entity.ID} does not exist");


            string state = "UNKNOWN";

            if (existing != null)
            {
                if (existing.DocumentStatus == DocumentStatus.APPROVED)
                    state = "ISSUED";
                else if (existing.DocumentStatus == DocumentStatus.CANCELLED)
                    state = "CANCELLED";
            }

            if (existing != null && (existing.DocumentStatus == DocumentStatus.APPROVED || existing.DocumentStatus == DocumentStatus.CANCELLED))
                throw new SafetyDocumentInvalidStateException($"Safety document is in {state} state and cannot be edited.");


            entity.User = null;

            existing.Update(_mapper.Map<SafetyDocument>(entity));

            _dbContext.SaveChanges();

            return _mapper.Map<SafetyDocumentDto>(existing);
        }

        public ChecklistDto UpdateSafetyDocumentChecklist(ChecklistDto checklistDto)
        {
            SafetyDocument sf = _dbContext.SafetyDocuments.Find(checklistDto.SafetyDocumentId);

            if (sf == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {checklistDto.SafetyDocumentId} does not exist.");

            sf.UpdateChecklist(checklistDto);

            _dbContext.SaveChanges();

            return new ChecklistDto() { SafetyDocumentId = sf.ID, TagsRemoved = sf.TagsRemoved, Ready = sf.Ready, GroundingRemoved = sf.GroundingRemoved, OperationCompleted = sf.OperationCompleted };

        }

        private void ValidateSafetyDocument(SafetyDocumentDto entity)
        {
            if (_dbContext.WorkPlans.Find(entity.WorkPlanID) == null)
            {
                throw new WorkPlanNotFoundException($"Attached work plan with id {entity.WorkPlanID} does not exist.");
            }

            if (_dbContext.Users.Find(entity.UserID) == null)
                throw new UserNotFoundException($"Attached user with id {entity.UserID} does not exist.");

            if (!Enum.IsDefined(typeof(DocumentStatus), entity.DocumentStatus))
                throw new InvalidSafetyDocumentException("Undefined document status!");

            if (!Enum.IsDefined(typeof(WorkType), entity.DocumentType))
                throw new InvalidSafetyDocumentException("Undefined document type!");

            SafetyDocument sf = _dbContext.SafetyDocuments.Find(entity.ID);

          


            if (_dbContext.WorkPlans.Any(x => x.ID == entity.WorkPlanID) == false)
                throw new WorkPlanNotFoundException($"Work plan with id = {entity.WorkPlanID} does not exists!");

            if (entity.Details == null || entity.Details.Length > 100)
                throw new SafetyDocumentInvalidStateException($"Details must be at most 100 characters long and is required.");

            if (entity.Notes == null && entity.Notes.Length > 100)
                throw new SafetyDocumentInvalidStateException($"Note must be at most 100 characters long.");


            if (entity.Phone != null && entity.Phone.Length > 30)
                throw new SafetyDocumentInvalidStateException($"Phone must be at most 30 characters long.");

         
        }

    }
}
