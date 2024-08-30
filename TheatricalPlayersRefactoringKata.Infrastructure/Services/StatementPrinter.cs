using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services
{
    public class StatementPrinter
    {
        private Dictionary<string, IStatementGenerator> _generators = new Dictionary<string, IStatementGenerator>();

        public StatementPrinter()
        {
            _generators.Add("text", new TextStatementGenerator());
            _generators.Add("xml", new XMLStatementGenerator());
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays, string format)
        {
            return _generators[format].GenerateStatement(invoice, plays);
        }
    }
}