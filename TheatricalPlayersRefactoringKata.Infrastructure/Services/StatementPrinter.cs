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
            try
            {
                return _generators[format].GenerateStatement(invoice, plays);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Not supported type: {format}");
            }
        }
    }
}