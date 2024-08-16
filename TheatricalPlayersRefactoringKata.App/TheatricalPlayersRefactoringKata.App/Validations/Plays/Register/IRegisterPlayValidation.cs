using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
public interface IRegisterPlayValidation
{
    Task<ResponsePlay> Execute(RequestPlay request);
}
