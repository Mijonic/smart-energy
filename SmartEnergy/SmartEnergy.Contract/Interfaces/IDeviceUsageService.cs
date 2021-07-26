using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IDeviceUsageService : IGenericService<DeviceUsageDto>
    {
        Task<bool> CopyIncidentDevicesToWorkRequest(int incidentID, int workRequestID);

        Task<bool> CopyIncidentDevicesToSafetyDocument(int workPlanId, int safetyDocumentId);

        Task<bool> UpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId);

        Task<bool> DeleteDeviceUsage(int id);

        Task<DeviceUsageDto> GetDeviceUsage(int id);

        Task<List<DeviceUsageDto>> GetAllDeviceUsages();

        Task<DeviceUsageDto> InsertDeviceUsage(DeviceUsageDto entity);

        Task<DeviceUsageDto> UpdateDeviceUsage(DeviceUsageDto entity);

        
    }
}
