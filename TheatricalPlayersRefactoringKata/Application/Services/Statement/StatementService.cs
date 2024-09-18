using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.Services.Statement
{
    public class StatementService
    {
        private readonly IStatementGeneratorStrategy _statement;

        public StatementService(IStatementGeneratorStrategy statement)
        {
            _statement = statement;
        }

        public async Task<string> Execute(Invoice invoice)
        {
            return await _statement.GenerateStatement(invoice);
        }
    }
}
