
using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.DeviceUsageAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.DeviceUsageAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
     


            CreateMap<DeviceUsage, DeviceUsageDto>();
            CreateMap<DeviceUsageDto, DeviceUsage>();



        }
    }
}
