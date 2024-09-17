using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Interfaces;
public interface IStatementFormatter
{
    Task<string> PrintAsync(Invoice invoice);
}
