using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Plays.Register
{
    public interface IRegisterPlayUseCase
    {
        Task<ResponseRegisterPlayJson> Execute(RequestPlayJson request);
    }
}
