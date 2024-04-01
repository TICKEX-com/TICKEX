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
                config.CreateMap<EventDTO, Event>().ReverseMap();
            });
        return mappingConfig;
    }
}