using Aplication.DTO;
using AutoMapper;
using CrossCutting;
using TheatricalPlayersRefactoringKata.Entity;

namespace Aplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PlayDto, Play>()
                .ForMember(play => play.Type, map => map.MapFrom(dto  => dto.Type.ToString()))
                .ReverseMap()
                .ForMember(dto => dto.Type, map => map.MapFrom(play => (PlayType)Enum.Parse(typeof(PlayType), 
                play.Type)));

            CreateMap<Play, PlayDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (PlayType)Enum.Parse(typeof(PlayType),
                src.Type))).ReverseMap()
                .ForMember(dest => dest.Performance, opt => opt.Ignore());
            
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Performance, PerformanceDto>().ReverseMap();
        }
    }
}
