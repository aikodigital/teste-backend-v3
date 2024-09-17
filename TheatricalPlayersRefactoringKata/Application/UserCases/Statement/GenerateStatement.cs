using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Application.UserCases.Statement
{
    public class GenerateStatement
    {
        private readonly IStatement _statement;

        public GenerateStatement(IStatement statement)
        {
            _statement = statement;
        }

        public string Execute(Invoice invoice)
        {
            return _statement.Print(invoice);
        }
    }
}
