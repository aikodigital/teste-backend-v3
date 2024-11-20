using TheatricalPlayersRefactoringKata.Application.Formatters;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Factories
{
    public static class StatementFormatterFactory
    {
        public static IStatementFormatter Create(string format)
        {
            return format.ToLower() switch
            {
                "text" => new TextStatementFormatter(),
                "xml" => new XmlStatementFormatter(),
                _ => throw new ArgumentException($"Format '{format}' is not supported.")
            };
        }
    }
}
