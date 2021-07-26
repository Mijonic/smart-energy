using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;

namespace SmartEnergy.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<LocationDto> GetAllLocations()
        {
            return _mapper.Map<List<LocationDto>>(_dbContext.Location.ToList());
        }

        public LocationDto GetLocationById(int locationId)
        {
            throw new NotImplementedException();
        }
    }
}
