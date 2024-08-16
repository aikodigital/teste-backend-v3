using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;
public interface IGetAllPlaysValidation
{
    Task<ResponsePlays> Execute();
}
