using TheatricalPlayersRefactoringKata.Domain.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IStatementPrinter
{
    public string TextPrint(Invoice invoice, Dictionary<string, Play> plays);
    public string XmlPrint();
}