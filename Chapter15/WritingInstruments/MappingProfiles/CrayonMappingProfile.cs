using System;

using AutoMapper;

using WritingExample.Data;
using WritingExample.Models;

namespace WritingInstruments.MappingProfiles
{
    public class CrayonMappingProfile : Profile
    {
        public CrayonMappingProfile()
        {
            CreateMap<Crayon, CrayonViewModel>()
                .ForMember(dest => dest.Color, m => m.MapFrom(src => src.HTMLColor))
                .ReverseMap();
        }
    }
}
