using AutoMapper;
using Newtonsoft.Json;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models.Dtos;
using TheatricalPlayersRefactoringKata.Entities;
using Formatting = Newtonsoft.Json.Formatting;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;

public class JsonFormatterAdapter : IFormatterAdapter
{
    private readonly IMapper _mapper;

    public JsonFormatterAdapter(IMapper mapper) 
    {
        _mapper = mapper;
    }
    public string Format(Statement statement)
    {
        var statementDto = _mapper.Map<StatementDto>(statement);

        return JsonConvert.SerializeObject(statementDto, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        });
    }
}
