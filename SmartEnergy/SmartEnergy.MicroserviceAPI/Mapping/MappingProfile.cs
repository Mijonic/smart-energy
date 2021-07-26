
using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.MicroserviceAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Icon, IconDto>();
            CreateMap<Settings, SettingsDto>();

            CreateMap<IconDto, Icon>();
            CreateMap<SettingsDto, Settings>();

            CreateMap<CrewDto, Crew>();
            CreateMap<Crew, CrewDto>();

            CreateMap<UserDto, User>()
                .ForMember(mem => mem.UserStatus, op => op.MapFrom(o => o.UserStatus))
                .ForMember(mem => mem.UserType, op => op.MapFrom(o => o.UserType)); 
            CreateMap<User, UserDto>()
                .ForMember(mem => mem.UserStatus, op => op.MapFrom(o => o.UserStatus))
                .ForMember(mem => mem.UserType, op => op.MapFrom(o => o.UserType));

     

            CreateMap<MultimediaAttachmentDto, MultimediaAttachment>();
            CreateMap<MultimediaAttachment, MultimediaAttachmentDto>();

           

            CreateMap<WorkRequestDto, WorkRequest>()
                .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
                .ForMember(mem => mem.DocumentType, op => op.MapFrom(o => o.DocumentType));

            CreateMap<StateChangeHistory, StateChangeHistoryDto>()
                .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
                .ForMember(mem => mem.Name, op => op.MapFrom(o => o.User.Name))
                .ForMember(mem => mem.LastName, op => op.MapFrom(o => o.User.Lastname));

            CreateMap<WorkRequest, WorkRequestDto>()
                .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
                .ForMember(mem => mem.DocumentType, op => op.MapFrom(o => o.DocumentType));

            CreateMap<Incident, IncidentDto>()
               .ForMember(mem => mem.WorkType, op => op.MapFrom(o => o.WorkType))
               .ForMember(mem => mem.IncidentStatus, op => op.MapFrom(o => o.IncidentStatus));

            CreateMap<IncidentDto, Incident>()
                 .ForMember(mem => mem.WorkType, op => op.MapFrom(o => o.WorkType))
                 .ForMember(mem => mem.IncidentStatus, op => op.MapFrom(o => o.IncidentStatus));

            CreateMap<Resolution, ResolutionDto>()
                .ForMember(mem => mem.Cause, op => op.MapFrom(o => o.Cause))
                .ForMember(mem => mem.Subcause, op => op.MapFrom(o => o.Subcause))
                .ForMember(mem => mem.Material, op => op.MapFrom(o => o.Material))
                .ForMember(mem => mem.Construction, op => op.MapFrom(o => o.Construction));


             CreateMap<ResolutionDto, Resolution>()
                .ForMember(mem => mem.Cause, op => op.MapFrom(o => o.Cause))
                .ForMember(mem => mem.Subcause, op => op.MapFrom(o => o.Subcause))
                .ForMember(mem => mem.Material, op => op.MapFrom(o => o.Material))
                .ForMember(mem => mem.Construction, op => op.MapFrom(o => o.Construction));


            CreateMap<DeviceUsage, DeviceUsageDto>();
            CreateMap<DeviceUsageDto, DeviceUsage>();


            CreateMap<Call, CallDto>()
                .ForMember(mem => mem.CallReason, op => op.MapFrom(o => o.CallReason));

            CreateMap<CallDto, Call>()
                 .ForMember(mem => mem.CallReason, op => op.MapFrom(o => o.CallReason));


            CreateMap<Consumer, ConsumerDto>()
                .ForMember(mem => mem.AccountType, op => op.MapFrom(o => o.AccountType));

            CreateMap<ConsumerDto, Consumer>()
                 .ForMember(mem => mem.AccountType, op => op.MapFrom(o => o.AccountType));


            CreateMap<SafetyDocument, SafetyDocumentDto>()
              .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
              .ForMember(mem => mem.DocumentType, op => op.MapFrom(o => o.DocumentType));

            CreateMap<SafetyDocumentDto, SafetyDocument>()
              .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
              .ForMember(mem => mem.DocumentType, op => op.MapFrom(o => o.DocumentType));









        }
    }
}
