using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy
{
    public interface IStatementGeneratorStrategy
    {
        Task<string> GenerateStatement(Invoice invoice);
    }
}
