using AutoMapper;
using TheatricalPlayersRefactoringKata.Dtos;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Performance, PerformanceDto>().ReverseMap();
            CreateMap<Play, PlayDto>().ReverseMap();
        }
    }
}
