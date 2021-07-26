using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IDeviceService : IGenericService<DeviceDto>
    {

        Task<DeviceListDto> GetDevicesPaged(DeviceField sortBy, SortingDirection direction, int page, int perPage);
        Task<DeviceListDto> GetSearchDevicesPaged(DeviceField sortBy, SortingDirection direction, int page, int perPage, DeviceFilter type, DeviceField field, string searchParam);

        Task<DeviceDto> GetById(int id);

        Task<List<DeviceDto>> GetAllDevices();

        Task<DeviceDto> InsertDevice(DeviceDto entity);

        Task<DeviceDto> UpdateDevice(DeviceDto entity);




    }
}
