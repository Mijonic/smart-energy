using AutoMapper;
using Dapr.Client;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using SmartEnergy.Contract.CustomExceptions.SafetyDocument;
using SmartEnergy.Contract.CustomExceptions.Saga;
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
using System.Net;
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

        public async Task<SafetyDocumentDto> CompensateUpdateSafetyDocument(SafetyDocumentDto oldSafetyDocument)
        {
            SafetyDocument existing = await _dbContext.SafetyDocuments.FindAsync(oldSafetyDocument.ID);

            if (existing == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {oldSafetyDocument.ID} does not exist");

            oldSafetyDocument.User = null;

            var updated = -1;
            int currentRetry = 0;
            int maxRetry = 5;
            int delay = 5;

            do
            {

                try
                {
                    existing.Update(_mapper.Map<SafetyDocument>(oldSafetyDocument));
                    updated = await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {                  

                    currentRetry++;

             
                    if (currentRetry > maxRetry || !IsTransient(ex))
                    {
                        throw new SagaException("Distributed transaction failed, server is under maintenance!");
                    }
                }

               
               if( updated <= 0)
               {
                    await Task.Delay(TimeSpan.FromSeconds(delay));
                    delay += 5;
               }
            

            

            } while (updated <= 0);

            SafetyDocumentDto toReturn = _mapper.Map<SafetyDocumentDto>(existing);
            toReturn.SagaIndicator = 0;

            return toReturn;
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

        public async Task<SafetyDocumentDto> UpdateSafetyDocument(SafetyDocumentDto entity)
        {

            SafetyDocument existing = await _dbContext.SafetyDocuments.FindAsync(entity.ID);

            if (existing == null)
                throw new SafetyDocumentNotFoundException($"Safety document with id {entity.ID} does not exist");

            entity.User = null;

            existing.Update(_mapper.Map<SafetyDocument>(entity));

            await _dbContext.SaveChangesAsync();

            SafetyDocumentDto updated = _mapper.Map<SafetyDocumentDto>(existing);
            updated.SagaIndicator = 1;

            return updated;


        }

            

        public async Task<bool> UpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId)
        {
            bool updated = false;
         
            try
            {

                updated = await _daprClient.InvokeMethodAsync<bool>(HttpMethod.Put, "smartenergydeviceusage", $"/api/device-usage/work-plan/{workPlanId}/safety-document/{safetyDocumentId}");

            }
         
            catch (Exception e)
            {
                return false;
            }



            return updated;
        }



        private bool IsTransient(Exception ex)
        {
           
            if (ex is SqlException)
                return true;

            if (ex is DbUpdateException)
                return true;

            if (ex is InvalidOperationException)
                return true;

            if (ex is ObjectDisposedException)
                return true;

            var webException = ex as WebException;
            if (webException != null)
            {
                
                return new[] {WebExceptionStatus.ConnectionClosed,
                  WebExceptionStatus.Timeout,
                  WebExceptionStatus.RequestCanceled }.
                        Contains(webException.Status);
            }



           
            return false;
        }


    }
}
