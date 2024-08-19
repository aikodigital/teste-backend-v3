using AutoMapper;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Repos;

namespace TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;

public class GetAllPlaysValidation : IGetAllPlaysValidation
{
    private readonly IPlay _repo;
    private readonly IMapper _mapper;
    public GetAllPlaysValidation(IPlay repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ResponsePlays> Execute()
    {
        var result = await _repo.GetAllPlays();

        return new ResponsePlays
        {
            Plays = _mapper.Map<List<ResponsePlay>>(result)
        };
    }
}
