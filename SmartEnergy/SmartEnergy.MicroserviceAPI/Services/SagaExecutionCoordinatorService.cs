using AutoMapper;
using Dapr.Client;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using SmartEnergy.Contract.CustomExceptions.SafetyDocument;
using SmartEnergy.Contract.CustomExceptions.WorkPlan;
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
using System.Threading.Tasks;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class SagaExecutionCoordinatorService : ISagaExecutionCoordinatorService
    {
        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;  
        private readonly DaprClient _daprClient;

        public SagaExecutionCoordinatorService(MicroserviceDbContext dbContext, IMapper mapper, DaprClient daprClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _daprClient = daprClient;
        }

        public async Task<SafetyDocumentDto> CompensateUpdateSafetyDocument(SafetyDocumentDto entity, SafetyDocumentDto oldValue)
        {
            SafetyDocument toUpdate = _mapper.Map<SafetyDocument>(oldValue);

            entity.User = null;


            toUpdate.Update(_mapper.Map<SafetyDocument>(entity));

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SafetyDocumentDto>(toUpdate);
        }

        public async Task<bool> CompensateUpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId)
        {
            bool updated = false;

            try
            {

                updated = await _daprClient.InvokeMethodAsync<bool>(HttpMethod.Put, "smartenergydeviceusage", $"/api/device-usage/work-plan/{workPlanId}/safety-document/{safetyDocumentId}");

            }
            catch (InvocationException invocationException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw new DeviceUsageNotFoundException("Device usage service is unavailable right now.");
            }



            return updated;
        }

        public async Task<SafetyDocumentDto> UpdateSafetyDocument(SafetyDocumentDto entity, SafetyDocumentDto existing)
        {
           
            SafetyDocument toUpdate = _mapper.Map<SafetyDocument>(existing);

            entity.User = null;


            toUpdate.Update(_mapper.Map<SafetyDocument>(entity));

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SafetyDocumentDto>(toUpdate);

          
        }

            

        public async Task<bool> UpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId)
        {
            bool updated = false;
         
            try
            {

                updated = await _daprClient.InvokeMethodAsync<bool>(HttpMethod.Put, "smartenergydeviceusage", $"/api/device-usage/work-plan/{workPlanId}/safety-document/{safetyDocumentId}");

            }
            catch (InvocationException invocationException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw new DeviceUsageNotFoundException("Device usage service is unavailable right now.");
            }



            return updated;
        }

   


     
    }
}
