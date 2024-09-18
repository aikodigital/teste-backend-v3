using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Formatters
{
    public class StatementFormatterFactory : IStatementFormatterFactory
    {
        public IStatementFormatter CreateFormatter(bool asXml)
        {
            return asXml ? new XmlStatementFormatter() : new TextStatementFormatter();
        }
    }
}
