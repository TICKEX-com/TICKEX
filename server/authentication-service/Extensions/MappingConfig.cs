using authentication_service.DTOs;
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