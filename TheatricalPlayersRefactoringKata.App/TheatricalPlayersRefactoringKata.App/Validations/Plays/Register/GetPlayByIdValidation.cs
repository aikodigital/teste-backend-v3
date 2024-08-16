using AutoMapper;
using TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Repos;
using TheatricalPlayersRefactoringKata.Exception;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public class GetPlayByIdValidation : IGetPlayByIdValidation
{
    private readonly IPlay _repos;
    private readonly IMapper _mapper;

    public GetPlayByIdValidation(IPlay repos, IMapper mapper)
    {
        _repos = repos;
        _mapper = mapper;
    }

    public async Task<ResponsePlay> Execute(long id)
    {
        var result = await _repos.GetById(id);

        if (result is null)
        {
            throw new NotFoundException(ResourceErrorMessages.PLAY_NOT_FOUND);
        }

        return _mapper.Map<ResponsePlay>(result);
    }
}
