using TheatricalPlayers.Core.Entities;

namespace TheatricalPlayers.Core.Interfaces.Statements;

public interface IStatementPrinterHandler
{
    public string PrintTxt(Invoice invoice, List<Play> plays);
    public string PrintXml(Invoice invoice, List<Play> plays);
}