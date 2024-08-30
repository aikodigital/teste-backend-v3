using AutoMapper;
using TheatricalPlayersRefactoringKata.Application.Models.Dtos;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Statement, StatementDto>();
        CreateMap<StatementItem, StatementItemDto>();
    }
}
