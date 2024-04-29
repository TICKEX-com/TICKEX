<<<<<<< HEAD
﻿using authentication_service.DTOs;
using authentication_service.Entities;
using AutoMapper;

namespace authentication_service.Extensions
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>().ReverseMap();
                cfg.CreateMap<UserDto, OrganizerDto>().ReverseMap();
                cfg.CreateMap<RegisterReqOrganizerDto, OrganizerDto>().ReverseMap();
                cfg.CreateMap<UpdateReqOrganizerDto, OrganizerDto>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
=======
﻿using authentication_service.DTOs;
using authentication_service.Entities;
using AutoMapper;

namespace authentication_service.Extensions
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Mapping de Id
                   .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName)) // Mapping de Username
                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) // Mapping de Email
                   .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)) // Mapping de PhoneNumber
                   .ForMember(dest => dest.certificat, opt => opt.MapFrom(src => src.certificat)) // Mapping de PhoneNumber
                   .ForMember(dest => dest.Role, opt => opt.Ignore()); // Ignorer le mapping de Role car il n'existe pas dans User
            });
            return mappingConfig;
        }
    }
}
>>>>>>> authentication
