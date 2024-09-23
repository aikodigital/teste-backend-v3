using AutoMapper;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Domain.Entities;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestDomain();
    }

    private void RequestDomain()
    {
        CreateMap<Statement,StatementToXml>();
        CreateMap<Item,ItemToXMl>()
        .ForMember(dest => dest.Name,opt=> opt.Ignore());
  
    }
}