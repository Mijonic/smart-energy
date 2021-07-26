using AutoMapper;
using Dapr.Client;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.DevicesAPI.DomainModel;
using SmartEnergy.DevicesAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartEnergy.DevicesAPI.Service
{
    public class DeviceService : IDeviceService
    {

        private readonly DeviceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly DaprClient _daprClient;

        public DeviceService(DeviceDbContext dbContext, IMapper mapper, DaprClient daprClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _daprClient = daprClient;
        }



        public void Delete(int id)
        {
            Device device = _dbContext.Devices.FirstOrDefault(x => x.ID.Equals(id));

            if (device == null)
                throw new DeviceNotFoundException($"Device with Id = {id} does not exists!");

            _dbContext.Devices.Remove(device);
            _dbContext.SaveChanges();
        }

        public async Task<DeviceDto> GetById(int id)
        {
            // return _mapper.Map<DeviceDto>(_dbContext.Devices.Include("Location").FirstOrDefault(x => x.ID == id));

            Device device = _dbContext.Devices.FirstOrDefault(x => x.ID == id);
            DeviceDto deviceDto = new DeviceDto();
            
            if(device != null)
            {
                deviceDto = _mapper.Map<DeviceDto>(device);

                try
                {
                    LocationDto location = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{device.LocationID}");
                    deviceDto.Location = location;
                    
                }
                catch(Exception e)
                {
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }
            }


            return deviceDto;
        }

        public async Task<List<DeviceDto>> GetAllDevices()
        {
            //return _mapper.Map<List<DeviceDto>>(_dbContext.Devices.Include("Location").ToList());
            //return _mapper.Map<List<DeviceDto>>(_dbContext.Devices.ToList());

            List<Device> devices = _dbContext.Devices.ToList();
            List<DeviceDto> returnDevices = new List<DeviceDto>();

            LocationDto locationDto = new LocationDto();
            DeviceDto deviceDto = new DeviceDto();

            foreach(Device d in devices)
            {
                deviceDto = _mapper.Map<DeviceDto>(d);

                try
                {
                    locationDto = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{d.LocationID}");
                    deviceDto.Location = locationDto;

                }
                catch (Exception e)
                {
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }

                returnDevices.Add(deviceDto);


            }

            return returnDevices; 

        }

        public async Task<DeviceListDto> GetDevicesPaged(DeviceField sortBy, SortingDirection direction, int page, int perPage)
        {
            // IQueryable<Device> devicesPaged = _dbContext.Devices.Include(x => x.Location).AsQueryable();
            IQueryable<Device> devicesPaged = _dbContext.Devices.AsQueryable();


            devicesPaged = SortDevices(devicesPaged, sortBy, direction);

            int resourceCount = devicesPaged.Count();
            devicesPaged = devicesPaged.Skip(page * perPage)
                                    .Take(perPage);


            List<DeviceDto> pagedDevices = _mapper.Map<List<DeviceDto>>(devicesPaged.ToList());
            LocationDto locationDto = new LocationDto();
            
            foreach(DeviceDto device in pagedDevices)
            {
                try
                {
                    locationDto = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{device.LocationID}");
                    device.Location = locationDto;

                }
                catch (Exception e)
                {
                    device.Location = new LocationDto();
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }
            }

            DeviceListDto returnValue = new DeviceListDto()
            {
                Devices = pagedDevices,
                TotalCount = resourceCount
            };

            return returnValue;
        }

        public async Task<DeviceListDto> GetSearchDevicesPaged(DeviceField sortBy, SortingDirection direction, int page, int perPage, DeviceFilter type, DeviceField field, string searchParam)
        {
            // IQueryable<Device> devices = _dbContext.Devices.Include(x => x.Location).AsQueryable();
            IQueryable<Device> devices = _dbContext.Devices.AsQueryable();

            devices = FilterDevicesByDeviceFilter(devices, type);
            devices = SearchDevices(devices, field, searchParam);
            devices = SortDevices(devices, sortBy, direction);

            int resourceCount = devices.Count();
            devices = devices.Skip(page * perPage)
                                    .Take(perPage);

            List<DeviceDto> searchedDevices = _mapper.Map<List<DeviceDto>>(devices.ToList());
            LocationDto locationDto = new LocationDto();

            foreach (DeviceDto device in searchedDevices)
            {
                try
                {
                    locationDto = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{device.LocationID}");
                    device.Location = locationDto;

                }
                catch (Exception e)
                {
                    device.Location = new LocationDto();
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }
            }

            DeviceListDto returnValue = new DeviceListDto()
            {
                Devices = searchedDevices,
                TotalCount = resourceCount
            };

            return returnValue;
        }

        public async Task<DeviceDto> InsertDevice(DeviceDto entity)
        {
            Device newDevice = _mapper.Map<Device>(entity);

            newDevice.ID = 0;
           // newDevice.Location = null;
            newDevice.Timestamp = DateTime.Now;

            //if (entity.Name.Trim().Equals("") || entity.Name == null)
            //    throw new InvalidDeviceException("You have to enter device name!");

            if (!Enum.IsDefined(typeof(DeviceType), entity.DeviceType))
                throw new InvalidDeviceException("Undefined device type!");

            //if (_dbContext.Location.Any(x => x.ID == entity.LocationID) == false)
            //    throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");

           
            try
            {
                LocationDto locationDto = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{entity.LocationID}");
                if (locationDto == null)
                    throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");

            }
            catch (Exception e)
            {

                throw new LocationNotFoundException("Location service is unavailable right now.");
            }

            Device deviceWithMaxCounter = _dbContext.Devices.FirstOrDefault(x => x.DeviceCounter == _dbContext.Devices.Max(y => y.DeviceCounter));

            if (deviceWithMaxCounter == null)
            {
                newDevice.Name = $"{newDevice.DeviceType.ToString().Substring(0, 3)}1";
                newDevice.DeviceCounter = 1;
            }
            else
            {
                newDevice.Name = $"{newDevice.DeviceType.ToString().Substring(0, 3)}{deviceWithMaxCounter.DeviceCounter + 1}";
                newDevice.DeviceCounter = deviceWithMaxCounter.DeviceCounter + 1;
            }



            _dbContext.Devices.Add(newDevice);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceDto>(newDevice);



        }

        public async  Task<DeviceDto> UpdateDevice(DeviceDto entity)
        {

            Device updatedDevice = _mapper.Map<Device>(entity);
            Device oldDevice = _dbContext.Devices.FirstOrDefault(x => x.ID.Equals(updatedDevice.ID));





            //updatedDevice.Location = null;



            if (oldDevice == null)
                throw new DeviceNotFoundException($"Device with Id = {updatedDevice.ID} does not exists!");

            if (updatedDevice.Timestamp < oldDevice.Timestamp)
                throw new InvalidDeviceException("You have tried to modify outdated device. Please, try again.");

            //if (updatedDevice.Name.Trim().Equals("") || updatedDevice.Name == null)
            //    throw new InvalidDeviceException("You have to enter device name!");

            //if (_dbContext.Location.Where(x => x.ID.Equals(updatedDevice.LocationID)) == null)
            //    throw new LocationNotFoundException($"Location with id = {updatedDevice.LocationID} does not exists!");

            try
            {
                LocationDto locationDto = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{entity.LocationID}");
                if (locationDto == null)
                    throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");

            }
            catch (Exception e)
            {

                throw new LocationNotFoundException("Location service is unavailable right now.");
            }


            updatedDevice.Name = $"{updatedDevice.DeviceType.ToString().Substring(0, 3)}{updatedDevice.DeviceCounter}";


            oldDevice.UpdateDevice(updatedDevice);
            _dbContext.SaveChanges();

            return _mapper.Map<DeviceDto>(oldDevice);
        }



        private IQueryable<Device> FilterDevicesByDeviceFilter(IQueryable<Device> devices, DeviceFilter type)
        {



            //Filter by status, ignore if ALL
            switch (type)
            {
                case DeviceFilter.POWER_SWITCH:
                    return devices.Where(x => x.DeviceType == DeviceType.POWER_SWITCH);
                case DeviceFilter.FUSE:
                    return devices.Where(x => x.DeviceType == DeviceType.FUSE);
                case DeviceFilter.TRANSFORMER:
                    return devices.Where(x => x.DeviceType == DeviceType.TRANSFORMER);
                case DeviceFilter.DISCONNECTOR:
                    return devices.Where(x => x.DeviceType == DeviceType.DISCONNECTOR);
            }

            return devices;
        }

        private IQueryable<Device> SortDevices(IQueryable<Device> devices, DeviceField sortBy, SortingDirection direction)
        {
            //Sort
            if (direction == SortingDirection.asc)
            {
                switch (sortBy)
                {
                    case DeviceField.ID:
                        return devices.OrderBy(x => x.ID);
                    case DeviceField.NAME:
                        return devices.OrderBy(x => x.Name);
                    case DeviceField.TYPE:
                        return devices.OrderBy(x => x.DeviceType);
                    //case DeviceField.ADDRESS:
                    //    return devices.OrderBy(x => x.Location.City)
                    //                   .ThenBy(x => x.Location.Street)
                    //                   .ThenBy(x => x.Location.Number);
                    //case DeviceField.COORDINATES:
                    //    return devices.OrderBy(x => x.Location.Latitude)
                    //                          .ThenBy(x => x.Location.Longitude);

                }

            }
            else
            {
                switch (sortBy)
                {

                    case DeviceField.ID:
                        return devices.OrderByDescending(x => x.ID);
                    case DeviceField.NAME:
                        return devices.OrderByDescending(x => x.Name);
                    case DeviceField.TYPE:
                        return devices.OrderByDescending(x => x.DeviceType);
                    //case DeviceField.ADDRESS:
                    //    return devices.OrderByDescending(x => x.Location.City)
                    //                   .ThenBy(x => x.Location.Street)
                    //                   .ThenBy(x => x.Location.Number);
                    //case DeviceField.COORDINATES:
                    //    return devices.OrderByDescending(x => x.Location.Latitude)
                    //                          .ThenBy(x => x.Location.Longitude);


                }

            }

            return devices;
        }


        private IQueryable<Device> SearchDevices(IQueryable<Device> devices, DeviceField field, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam)) //Ignore empty search
                return devices;


            switch (field)
            {
                case DeviceField.ID:
                    return devices.Where(x => x.ID.ToString().Trim().ToLower().Equals(searchParam.Trim().ToLower()));
                case DeviceField.NAME:
                    return devices.Where(x => x.Name.Trim().ToLower().Contains(searchParam.Trim().ToLower()));
                case DeviceField.TYPE:
                    return devices.Where(x => x.DeviceType.ToString().Contains(searchParam.Trim().ToLower()));
                //case DeviceField.ADDRESS:
                //    return devices.Where(x => x.Location.City.Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                //                              x.Location.Street.Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                //                              x.Location.Number.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()));
                //case DeviceField.COORDINATES:
                //    return devices.Where(x => x.Location.Longitude.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                //                         x.Location.Latitude.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()));




            }

            return devices;



        }

        public List<DeviceDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public DeviceDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public DeviceDto Insert(DeviceDto entity)
        {
            throw new NotImplementedException();
        }

        public DeviceDto Update(DeviceDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
