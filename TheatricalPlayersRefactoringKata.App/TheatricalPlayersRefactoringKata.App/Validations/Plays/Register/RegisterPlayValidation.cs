using AutoMapper;
using TheatherPlayersInfra;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Repos;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;

namespace TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
public class RegisterPlayValidation : IRegisterPlayValidation
{
    private readonly IPlay _repo;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public RegisterPlayValidation(
        IPlay repo,
        IUnityOfWork unityOfWork,
        IMapper mapper
        )
    {
        _repo = repo;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
    }

    public async Task<ResponsePlay> Execute(RequestPlay request)
    {
        Validate(request);

        var entity = _mapper.Map<Play>(request);
        await _repo.Add(entity);

        await _unityOfWork.Commit();
        return _mapper.Map<ResponsePlay>(entity);
    }

    private void Validate(RequestPlay request)
    {
        var validator = new RegisterPlayValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}