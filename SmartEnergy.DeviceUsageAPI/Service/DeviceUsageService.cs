using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.WorkPlan;
using SmartEnergy.Contract.CustomExceptions.SafetyDocument;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.DeviceUsageAPI.Infrastructure;
using SmartEnergy.DeviceUsageAPI.DomainModel;
using System.Threading.Tasks;
using Dapr.Client;
using System.Net.Http;

namespace SmartEnergy.DeviceUsageAPI.Services
{
    public class DeviceUsageService : IDeviceUsageService
    {

        private readonly DeviceUsageDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly DaprClient _daprClient;

        public DeviceUsageService(DeviceUsageDbContext dbContext, IMapper mapper, DaprClient daprClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _daprClient = daprClient;
        }

        //dodati za work plan nof
        public async Task<bool> CopyIncidentDevicesToSafetyDocument(int workPlanId, int safetyDocumentId)
        {
            //SafetyDocument sf = _dbContext.SafetyDocuments.Find(safetyDocumentId);


            SafetyDocumentDto sf = null;

            try
            {
                sf = await _daprClient.InvokeMethodAsync<SafetyDocumentDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/safety-documents/{safetyDocumentId}");
                
            }
            catch (Exception e)
            {
                throw new SafetyDocumentNotFoundException("Service for safety document manipulation is unavailable right now.");
            }


            if (sf == null)
                throw new SafetyDocumentNotFoundException($"Safety document with ID {safetyDocumentId} does not exist");

            List<DeviceUsage> usages = _dbContext.DeviceUsage.Where(x => x.WorkPlanID == workPlanId && x.SafetyDocumentID == null).ToList();



            foreach (DeviceUsage deviceUsage in usages)
            {
                deviceUsage.SafetyDocumentID = safetyDocumentId;
            }

            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> CopyIncidentDevicesToWorkRequest(int incidentID, int workRequestID)
        {

            WorkRequestDto wr = null;

            try
            {
                wr = await _daprClient.InvokeMethodAsync<WorkRequestDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/work-requests/{workRequestID}");

            }
            catch (Exception e)
            {
                throw new WorkRequestNotFound("Service for safety document manipulation is unavailable right now.");
            }


            if (wr == null)
                throw new WorkRequestNotFound($"Work request with ID {workRequestID} does not exist");

            List<DeviceUsage> usages = _dbContext.DeviceUsage.Where(x => x.IncidentID == incidentID).ToList();

            foreach (DeviceUsage deviceUsage in usages)
            {
                deviceUsage.WorkRequestID = workRequestID;
            }

            _dbContext.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteDeviceUsage(int id)
        {
            DeviceUsage deviceUsage = _dbContext.DeviceUsage.FirstOrDefault(x => x.ID.Equals(id));

            if (deviceUsage == null)
                throw new DeviceUsageNotFoundException($"DeviceUsage with Id = {id} does not exists!");

            _dbContext.DeviceUsage.Remove(deviceUsage);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DeviceUsageDto> GetDeviceUsage(int id)
        {
            DeviceUsage deviceUsage = await _dbContext.DeviceUsage.FirstOrDefaultAsync(x => x.ID == id);

            return  _mapper.Map<DeviceUsageDto>(deviceUsage);  
        
        }

        public async Task<List<DeviceUsageDto>> GetAllDeviceUsages()
        {

            List<DeviceUsage> devieUsages = await _dbContext.DeviceUsage.ToListAsync();

            return _mapper.Map<List<DeviceUsageDto>>(devieUsages);


        }

        public async Task<DeviceUsageDto> InsertDeviceUsage(DeviceUsageDto entity)
        {

            await ValidateDeviceUsage(entity);

            DeviceUsage newDeviceUsage = _mapper.Map<DeviceUsage>(entity);

            newDeviceUsage.ID = 0;
           
            newDeviceUsage.WorkPlanID = null;
            
            newDeviceUsage.WorkRequestID = null;
            
            newDeviceUsage.SafetyDocumentID = null;


            _dbContext.DeviceUsage.Add(newDeviceUsage);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DeviceUsageDto>(newDeviceUsage);

        }

        public async Task<DeviceUsageDto> UpdateDeviceUsage(DeviceUsageDto entity)
        {

            await ValidateDeviceUsage(entity);

            DeviceUsage updatedDeviceUsage = _mapper.Map<DeviceUsage>(entity);
            DeviceUsage oldDeviceUsage = _dbContext.DeviceUsage.FirstOrDefault(x => x.ID.Equals(updatedDeviceUsage.ID));



            if (oldDeviceUsage == null)
                throw new DeviceUsageNotFoundException($"Device usage with Id = {updatedDeviceUsage.ID} does not exists!");

            if (entity.WorkRequestID != null)
            {

                WorkRequestDto wr = null;

                try
                {
                    wr = await _daprClient.InvokeMethodAsync<WorkRequestDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/work-requests/{entity.WorkRequestID}");

                }
                catch (Exception e)
                {
                    throw new SafetyDocumentNotFoundException("Service for safety document manipulation is unavailable right now.");
                }


                if (wr == null)
                    throw new WorkRequestNotFound($"Work request with id = {entity.WorkRequestID} does not exists!");
            }


          
            if (entity.WorkPlanID != null)
            {

                WorkPlanDto wp = null;

                try
                {
                    wp = await _daprClient.InvokeMethodAsync<WorkPlanDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/work-plan/{entity.WorkPlanID}");

                }
                catch (Exception e)
                {
                    throw new SafetyDocumentNotFoundException("Service for work plans manipulation is unavailable right now.");
                }

                if (wp == null)
                    throw new WorkPlanNotFoundException($"Work plan with id = {entity.WorkPlanID} does not exists!");
            }

            if (entity.SafetyDocumentID != null)
            {
                SafetyDocumentDto sf = null;

                try
                {
                    sf = await _daprClient.InvokeMethodAsync<SafetyDocumentDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/safety-documents/{entity.SafetyDocumentID}");

                }
                catch (Exception e)
                {
                    throw new SafetyDocumentNotFoundException("Service for safety document manipulation is unavailable right now.");
                }


                if (sf == null)
                    throw new SafetyDocumentNotFoundException($"Safety document with ID {entity.SafetyDocumentID} does not exist");
            }




            oldDeviceUsage.UpdateDeviceUsage(updatedDeviceUsage);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceUsageDto>(oldDeviceUsage);

          
        }

        public async Task<bool> UpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId)
        {



            WorkPlanDto wp = null;

            try
            {
                wp = await _daprClient.InvokeMethodAsync<WorkPlanDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/work-plan/{workPlanId}");

            }
            catch (Exception e)
            {
                throw new SafetyDocumentNotFoundException("Service for work plans manipulation is unavailable right now.");
            }

            if (wp == null)
                throw new WorkPlanNotFoundException($"Work plan with id = {workPlanId} does not exists!");


            SafetyDocumentDto sf = null;

            try
            {
                sf = await _daprClient.InvokeMethodAsync<SafetyDocumentDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/safety-documents/{safetyDocumentId}");

            }
            catch (Exception e)
            {
                throw new SafetyDocumentNotFoundException("Service for safety document manipulation is unavailable right now.");
            }


            if (sf == null)
                throw new SafetyDocumentNotFoundException($"Safety document with ID {safetyDocumentId} does not exist");


            List<DeviceUsage> usages = _dbContext.DeviceUsage.Where(x => x.SafetyDocumentID == safetyDocumentId).ToList();



            foreach (DeviceUsage deviceUsage in usages)
            {
                deviceUsage.WorkPlanID = workPlanId;
            }

            _dbContext.SaveChanges();

            return true;
        }

        private async Task<bool> ValidateDeviceUsage(DeviceUsageDto entity)
        {

            if (entity.IncidentID == null)
                throw new InvalidDeviceUsageException("Incident id can not be null!");

            if (entity.IncidentID != null)
            {


                IncidentDto incidendDto = null;

                try
                {
                    incidendDto = await _daprClient.InvokeMethodAsync<IncidentDto>(HttpMethod.Get, "smartenergymicroservice", $"/api/incidents/{entity.IncidentID}");

                }
                catch (Exception e)
                {
                    throw new SafetyDocumentNotFoundException("Service for work plans manipulation is unavailable right now.");
                }

                if (incidendDto == null)
                    throw new IncidentNotFoundException($"Incident with id = {entity.IncidentID} does not exists!");
            }



            ////if (_dbContext.Devices.Any(x => x.ID == entity.DeviceID) == false)
            ////    throw new DeviceUsageNotFoundException($"Device with id = {entity.DeviceID} does not exists!");

            return true;


        }

        public List<DeviceUsageDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public DeviceUsageDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public DeviceUsageDto Insert(DeviceUsageDto entity)
        {
            throw new NotImplementedException();
        }

        public DeviceUsageDto Update(DeviceUsageDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
