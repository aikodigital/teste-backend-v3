namespace TheatricalPlayersRefactoringKata.Interfaces
{
    public interface IStatementFormatterFactory
    {
        IStatementFormatter CreateFormatter(bool asXml);
    }
}
