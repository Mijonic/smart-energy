using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.LocationAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.LocationAPI.Service
{
    public class LocationService : ILocationService
    {
        private readonly LocationDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationService(LocationDbContext dbContext, IMapper mapper)
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
            return _mapper.Map<LocationDto>(_dbContext.Location.FirstOrDefault(x => x.ID == locationId));
        }
    }
}
