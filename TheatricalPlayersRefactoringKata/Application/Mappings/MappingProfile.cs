using AutoMapper;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Models.Dtos;

namespace TheatricalPlayersRefactoringKata.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Statement, StatementDto>();
        CreateMap<StatementItem, StatementItemDto>();
    }
}
