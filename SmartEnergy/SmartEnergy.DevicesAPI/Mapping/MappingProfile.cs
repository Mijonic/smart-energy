using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.DevicesAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.DevicesAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

          

            CreateMap<DeviceDto, Device>()
                .ForMember(mem => mem.DeviceType, op => op.MapFrom(o => o.DeviceType));
            CreateMap<Device, DeviceDto>()
                .ForMember(mem => mem.DeviceType, op => op.MapFrom(o => o.DeviceType));

           


        }
    }
}
