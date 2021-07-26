using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.LocationAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.LocationAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LocationDto, Location>();
            CreateMap<Location, LocationDto>();




        }
    }
}
