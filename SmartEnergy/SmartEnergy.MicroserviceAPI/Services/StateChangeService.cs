using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions.SafetyDocument;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.MicroserviceAPI.DomainModels;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class StateChangeService : IStateChangeService
    {
        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthHelperService _authHelperService;

        public StateChangeService(MicroserviceDbContext dbContext, IMapper mapper, IAuthHelperService authHelperService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authHelperService = authHelperService;
        }

        public SafetyDocumentDto ApproveSafetyDocument(int safetyDocId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            SafetyDocument sd = _dbContext.SafetyDocuments.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == safetyDocId);

            if (sd == null)
                throw new SafetyDocumentNotFoundException($"Safety document  with ID {safetyDocId} does not exist");

            if (sd.DocumentStatus == DocumentStatus.APPROVED)
                throw new SafetyDocumentInvalidStateException($"Safety document is already approved.");

            if (sd.DocumentStatus == DocumentStatus.CANCELLED)
                throw new SafetyDocumentInvalidStateException($"Safety document is canceled and cannot be approved.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID,
                DocumentStatus = DocumentStatus.APPROVED
            };


            sd.StateChangeAnchor.StateChangeHistories.Add(state);
            sd.DocumentStatus = DocumentStatus.APPROVED;

            _dbContext.SaveChanges();

            return _mapper.Map<SafetyDocumentDto>(sd);
        }

        public WorkRequestDto ApproveWorkRequest(int workRequestId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == workRequestId);

            if (wr == null)
                throw new WorkRequestInvalidStateException($"Work request with ID {workRequestId} deos not exist");

            if(wr.DocumentStatus == DocumentStatus.APPROVED)
                throw new WorkRequestInvalidStateException($"Work request is already approved.");

            if (wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Work request is canceled and cannot be approved.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID, 
                DocumentStatus = DocumentStatus.APPROVED
            };


            wr.StateChangeAnchor.StateChangeHistories.Add(state);
            wr.DocumentStatus = DocumentStatus.APPROVED;

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(wr);
        }

        public SafetyDocumentDto CancelSafetyDocument(int safetyDocId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            SafetyDocument sd = _dbContext.SafetyDocuments.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == safetyDocId);

            if (sd == null)
                throw new SafetyDocumentNotFoundException($"Safety document  with ID {safetyDocId} does not exist");

            if (sd.DocumentStatus == DocumentStatus.APPROVED)
                throw new SafetyDocumentInvalidStateException($"Safety document is approved and cannot be cancelled.");

            if (sd.DocumentStatus == DocumentStatus.CANCELLED)
                throw new SafetyDocumentInvalidStateException($"Safety document is already canceled.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID,
                DocumentStatus = DocumentStatus.CANCELLED
            };


            sd.StateChangeAnchor.StateChangeHistories.Add(state);
            sd.DocumentStatus = DocumentStatus.CANCELLED;

            _dbContext.SaveChanges();
            return _mapper.Map<SafetyDocumentDto>(sd);
        }

        public WorkRequestDto CancelWorkRequest(int workRequestId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == workRequestId);

            if (wr == null)
                throw new WorkRequestInvalidStateException($"Work request with ID {workRequestId} deos not exist");

            if (wr.DocumentStatus == DocumentStatus.APPROVED)
                throw new WorkRequestInvalidStateException($"Work request is approved and cannot be canceled.");

            if (wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Work request is already canceled.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID,
                DocumentStatus = DocumentStatus.CANCELLED
            };


            wr.StateChangeAnchor.StateChangeHistories.Add(state);
            wr.DocumentStatus = DocumentStatus.CANCELLED;

            _dbContext.SaveChanges();
            return _mapper.Map<WorkRequestDto>(wr);
        }

        public SafetyDocumentDto DenySafetyDocument(int safetyDocId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            SafetyDocument sd = _dbContext.SafetyDocuments.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == safetyDocId);

            if (sd == null)
                throw new SafetyDocumentNotFoundException($"Safety document  with ID {safetyDocId} does not exist");

            if (sd.DocumentStatus == DocumentStatus.APPROVED)
                throw new SafetyDocumentInvalidStateException($"Safety document is approved and cannot be denied.");

            if (sd.DocumentStatus == DocumentStatus.CANCELLED)
                throw new SafetyDocumentInvalidStateException($"Safety document is canceled and cannot be denied.");

            if (sd.DocumentStatus == DocumentStatus.DENIED)
                throw new SafetyDocumentInvalidStateException($"Safety document is already denied.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID,
                DocumentStatus = DocumentStatus.DENIED
            };


            sd.StateChangeAnchor.StateChangeHistories.Add(state);
            sd.DocumentStatus = DocumentStatus.DENIED;

            _dbContext.SaveChanges();
            return _mapper.Map<SafetyDocumentDto>(sd);
        }

        public WorkRequestDto DenyWorkRequest(int workRequestId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == workRequestId);

            if (wr == null)
                throw new WorkRequestInvalidStateException($"Work request with ID {workRequestId} deos not exist");

            if (wr.DocumentStatus == DocumentStatus.APPROVED)
                throw new WorkRequestInvalidStateException($"Work request is approved and cannot be denied.");
            
            if (wr.DocumentStatus == DocumentStatus.DENIED)
                throw new WorkRequestInvalidStateException($"Work request is already denied.");

            if (wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Work request is canceled and cannot be denied.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID, 
                DocumentStatus = DocumentStatus.DENIED
            };


            wr.StateChangeAnchor.StateChangeHistories.Add(state);
            wr.DocumentStatus = DocumentStatus.DENIED;

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(wr);
        }

        public List<StateChangeHistoryDto> GetSafetyDocumentStateHistory(int safetyDocId)
        {
            SafetyDocument sd = _dbContext.SafetyDocuments.Include(x => x.StateChangeAnchor)
                                                             .ThenInclude(x => x.StateChangeHistories)
                                                             .ThenInclude(x => x.User)
                                                             .FirstOrDefault(x => x.ID == safetyDocId);

            if (sd == null)
                throw new SafetyDocumentNotFoundException($"Safety document  with ID {safetyDocId} does not exist");

            return _mapper.Map<List<StateChangeHistoryDto>>(sd.StateChangeAnchor.StateChangeHistories);
        }

        public List<StateChangeHistoryDto> GetWorkRequestStateHistory(int workRequestId)
        {
            WorkRequest workRequest = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                             .ThenInclude(x => x.StateChangeHistories)
                                                             .ThenInclude(x => x.User)
                                                             .FirstOrDefault(x => x.ID == workRequestId);

            if (workRequest == null)
                throw new WorkRequestNotFound($"Work request with ID {workRequestId} does not exist.");

            return _mapper.Map<List<StateChangeHistoryDto>>(workRequest.StateChangeAnchor.StateChangeHistories);
        }
    }
}
