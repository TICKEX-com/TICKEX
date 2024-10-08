﻿using AutoMapper;
using event_service.DTOs;
using event_service.Entities;

namespace event_service.Extensions;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(
            config =>
            {
                config.CreateMap<EventReqDto, Event>().ReverseMap();
                config.CreateMap<EventReqDto, PublishDto>().ReverseMap();
                config.CreateMap<EventsDto, Event>().ReverseMap();
                config.CreateMap<EventByIdDto, Event>().ReverseMap();
                config.CreateMap<ImageDto, Image>().ReverseMap();
                config.CreateMap<OrganizerDto, Organizer>().ReverseMap();
                config.CreateMap<CategoryDto, Category>().ReverseMap();
            });
        return mappingConfig;
    }
}