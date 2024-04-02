using AutoMapper;
using event_service.DTOs;
using event_service.Entities;

namespace event_service;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<EventReqDto, Event>().ReverseMap();
                config.CreateMap<EventsDto, Event>().ReverseMap();
                config.CreateMap<EventByIdDto, Event>().ReverseMap();
                config.CreateMap<ImageDto, Image>().ReverseMap();

            });
        return mappingConfig;
    }
}