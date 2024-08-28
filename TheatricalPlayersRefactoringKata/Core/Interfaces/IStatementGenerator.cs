using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IStatementGenerator
{
    Task <string> GenerateTextStatementAsync(Invoice invoice);
    Task <string> GenerateXmlStatementAsync(Invoice invoice);
}
