using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statment;


public interface IStatementFormatter
{
    Task<string> FormatAsync(Invoice invoice);
}


