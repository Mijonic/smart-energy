using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
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

namespace SmartEnergy.Service.Services
{
    public class DeviceUsageService : IDeviceUsageService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeviceUsageService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void CopyIncidentDevicesToSafetyDocument(int workPlanId, int safetyDocumentId)
        {
            SafetyDocument sf = _dbContext.SafetyDocuments.Find(safetyDocumentId);
            if (sf == null)
                throw new SafetyDocumentNotFoundException($"Safety document with ID {safetyDocumentId} does not exist");

            List<DeviceUsage> usages = _dbContext.DeviceUsages.Where(x => x.WorkPlanID == workPlanId).ToList();

         

            foreach (DeviceUsage deviceUsage in usages)
            {
                deviceUsage.SafetyDocumentID = safetyDocumentId;
            }

            _dbContext.SaveChanges();
        }

        public void CopyIncidentDevicesToWorkRequest(int incidentID, int workRequestID)
        {
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestID);
            if (wr == null)
                throw new WorkRequestNotFound($"Work request with ID {workRequestID} does not exist");
            List<DeviceUsage> usages = _dbContext.DeviceUsages.Where(x => x.IncidentID == incidentID).ToList();

            foreach(DeviceUsage deviceUsage in usages)
            {
                deviceUsage.WorkRequestID = workRequestID;
            }

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            DeviceUsage deviceUsage = _dbContext.DeviceUsages.FirstOrDefault(x => x.ID.Equals(id));

            if (deviceUsage == null)
                throw new DeviceUsageNotFoundException($"DeviceUsage with Id = {id} does not exists!");

            _dbContext.DeviceUsages.Remove(deviceUsage);
            _dbContext.SaveChanges();
        }

        public DeviceUsageDto Get(int id)
        {
            return _mapper.Map<DeviceUsageDto>(_dbContext.DeviceUsages.FirstOrDefault(x => x.ID == id));  // nisam vracao objekte incidenta itd
        }

        public List<DeviceUsageDto> GetAll()
        {
            return _mapper.Map<List<DeviceUsageDto>>(_dbContext.DeviceUsages.ToList());
        }

        public DeviceUsageDto Insert(DeviceUsageDto entity)
        {

            ValidateDeviceUsage(entity);

            DeviceUsage newDeviceUsage = _mapper.Map<DeviceUsage>(entity);

            newDeviceUsage.ID = 0;
            newDeviceUsage.WorkPlan = null;
            newDeviceUsage.WorkPlanID = null;
            newDeviceUsage.WorkRequest = null;
            newDeviceUsage.WorkRequestID = null;
            newDeviceUsage.SafetyDocument = null;
            newDeviceUsage.SafetyDocumentID = null;


            _dbContext.DeviceUsages.Add(newDeviceUsage);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceUsageDto>(newDeviceUsage);

        }

        public DeviceUsageDto Update(DeviceUsageDto entity)
        {

            ValidateDeviceUsage(entity);

            DeviceUsage updatedDeviceUsage = _mapper.Map<DeviceUsage>(entity);
            DeviceUsage oldDeviceUsage = _dbContext.DeviceUsages.FirstOrDefault(x => x.ID.Equals(updatedDeviceUsage.ID));



            if (oldDeviceUsage == null)
                throw new DeviceUsageNotFoundException($"Device usage with Id = {updatedDeviceUsage.ID} does not exists!");

            if (entity.WorkRequestID != null)
            {
                if (_dbContext.WorkRequests.Any(x => x.ID == entity.WorkRequestID) == false)
                    throw new WorkRequestNotFound($"Work request with id = {entity.WorkRequestID} does not exists!");
            }


            if (entity.WorkPlanID != null)
            {
                if (_dbContext.WorkPlans.Any(x => x.ID == entity.WorkPlanID) == false)
                    throw new WorkPlanNotFoundException($"Work plan with id = {entity.WorkRequestID} does not exists!");
            }

            if (entity.SafetyDocumentID != null)
            {
                if (_dbContext.SafetyDocuments.Any(x => x.ID == entity.SafetyDocumentID) == false)
                    throw new SafetyDocumentNotFoundException($"Safety document with id = {entity.SafetyDocumentID} does not exists!");
            }




            oldDeviceUsage.UpdateDeviceUsage(updatedDeviceUsage);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceUsageDto>(oldDeviceUsage);
        }

        private void ValidateDeviceUsage(DeviceUsageDto entity)
        {

            if (entity.IncidentID == null)
                throw new InvalidDeviceUsageException("Incident id can not be null!");

            if(entity.IncidentID != null)
            {
                if (_dbContext.Incidents.Any(x => x.ID == entity.IncidentID) == false)
                    throw new IncidentNotFoundException($"Incident with id = {entity.IncidentID} does not exists!");
            }

           

            if (_dbContext.Devices.Any(x => x.ID == entity.DeviceID) == false)
                throw new DeviceUsageNotFoundException($"Device with id = {entity.DeviceID} does not exists!");


        }
    }
}
