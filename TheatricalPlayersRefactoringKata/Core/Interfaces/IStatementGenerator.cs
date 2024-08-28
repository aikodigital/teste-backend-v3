using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IStatementGenerator
{
    Task <string> GenerateTextStatement(Invoice invoice);
    Task <string> GenerateXmlStatement(Invoice invoice);
}
