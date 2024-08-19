using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
public interface IGetPlayByIdValidation
{
    Task<ResponsePlay> Execute(long id);
}
