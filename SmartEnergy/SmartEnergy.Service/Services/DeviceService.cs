using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergyDomainModels;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.Enums;

namespace SmartEnergy.Service.Services
{
    public class DeviceService : IDeviceService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeviceService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        
        public void Delete(int id)
        {
            Device device = _dbContext.Devices.FirstOrDefault(x => x.ID.Equals(id));

            if (device == null)
                throw new DeviceNotFoundException($"Device with Id = {id} does not exists!");

            _dbContext.Devices.Remove(device);
            _dbContext.SaveChanges();
        }

        public DeviceDto Get(int id)
        {
            return _mapper.Map<DeviceDto>(_dbContext.Devices.Include("Location").FirstOrDefault(x => x.ID == id));
        }

        public List<DeviceDto> GetAll()
        {
            return _mapper.Map<List<DeviceDto>>(_dbContext.Devices.Include("Location").ToList());         

        }

        public DeviceListDto GetDevicesPaged(DeviceField sortBy, SortingDirection direction, int page, int perPage)
        {
            IQueryable<Device> devicesPaged = _dbContext.Devices.Include(x => x.Location).AsQueryable();


            devicesPaged = SortDevices(devicesPaged, sortBy, direction);

            int resourceCount = devicesPaged.Count();
            devicesPaged = devicesPaged.Skip(page * perPage)
                                    .Take(perPage);

            DeviceListDto returnValue = new DeviceListDto()
            {
                Devices = _mapper.Map<List<DeviceDto>>(devicesPaged.ToList()),
                TotalCount = resourceCount
            };

            return returnValue;
        }

        public DeviceListDto GetSearchDevicesPaged(DeviceField sortBy, SortingDirection direction, int page, int perPage, DeviceFilter type, DeviceField field, string searchParam)
        {
            IQueryable<Device> devices = _dbContext.Devices.Include(x => x.Location).AsQueryable();

            devices = FilterDevicesByDeviceFilter(devices, type);
            devices = SearchDevices(devices,field, searchParam);
            devices = SortDevices(devices, sortBy, direction);

            int resourceCount = devices.Count();
            devices = devices.Skip(page * perPage)
                                    .Take(perPage);

            DeviceListDto returnValue = new DeviceListDto()
            {
                Devices = _mapper.Map<List<DeviceDto>>(devices.ToList()),
                TotalCount = resourceCount
            };

            return returnValue;
        }

        public DeviceDto Insert(DeviceDto entity)
        {
            Device newDevice = _mapper.Map<Device>(entity);
            
            newDevice.ID = 0;
            newDevice.Location = null;
            newDevice.Timestamp = DateTime.Now;

            //if (entity.Name.Trim().Equals("") || entity.Name == null)
            //    throw new InvalidDeviceException("You have to enter device name!");

            if(!Enum.IsDefined(typeof(DeviceType), entity.DeviceType))
                throw new InvalidDeviceException("Undefined device type!");

            if (_dbContext.Location.Any(x => x.ID == entity.LocationID) == false)
                throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");

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

        public DeviceDto Update(DeviceDto entity)
        {

            Device updatedDevice = _mapper.Map<Device>(entity);
            Device oldDevice = _dbContext.Devices.FirstOrDefault(x => x.ID.Equals(updatedDevice.ID));


           


            updatedDevice.Location = null;



            if (oldDevice == null)
                throw new DeviceNotFoundException($"Device with Id = {updatedDevice.ID} does not exists!");

            if (updatedDevice.Timestamp < oldDevice.Timestamp)
                throw new InvalidDeviceException("You have tried to modify outdated device. Please, try again.");

            //if (updatedDevice.Name.Trim().Equals("") || updatedDevice.Name == null)
            //    throw new InvalidDeviceException("You have to enter device name!");

            if (_dbContext.Location.Where(x => x.ID.Equals(updatedDevice.LocationID)) == null)
                throw new LocationNotFoundException($"Location with id = {updatedDevice.LocationID} does not exists!");


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
                    case DeviceField.ADDRESS:
                        return devices.OrderBy(x => x.Location.City)
                                       .ThenBy(x => x.Location.Street)
                                       .ThenBy(x => x.Location.Number);
                    case DeviceField.COORDINATES:
                        return devices.OrderBy(x => x.Location.Latitude)
                                              .ThenBy(x => x.Location.Longitude);
                   
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
                    case DeviceField.ADDRESS:
                        return devices.OrderByDescending(x => x.Location.City)
                                       .ThenBy(x => x.Location.Street)
                                       .ThenBy(x => x.Location.Number);
                    case DeviceField.COORDINATES:
                        return devices.OrderByDescending(x => x.Location.Latitude)
                                              .ThenBy(x => x.Location.Longitude);

                   
                }

            }

            return devices;
        }


        private IQueryable<Device> SearchDevices(IQueryable<Device> devices, DeviceField field, string searchParam)
        {
            if (string.IsNullOrWhiteSpace(searchParam)) //Ignore empty search
                return devices;


            switch(field)
            {
                case DeviceField.ID:
                    return devices.Where(x => x.ID.ToString().Trim().ToLower().Equals(searchParam.Trim().ToLower()));
                case DeviceField.NAME:
                    return devices.Where(x => x.Name.Trim().ToLower().Contains(searchParam.Trim().ToLower()));
                case DeviceField.TYPE:
                    return devices.Where(x => x.DeviceType.ToString().Contains(searchParam.Trim().ToLower()));
                case DeviceField.ADDRESS:
                    return devices.Where(x => x.Location.City.Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                                              x.Location.Street.Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                                              x.Location.Number.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()));
                case DeviceField.COORDINATES:
                    return devices.Where(x => x.Location.Longitude.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()) ||
                                         x.Location.Latitude.ToString().Trim().ToLower().Contains(searchParam.Trim().ToLower()));




            }

            return devices;


          
        }







    }
}
