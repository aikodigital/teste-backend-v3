using TheatricalPlayers.Core.DataTransferObjects.StatementDTOs;
using TheatricalPlayers.Core.Models;

namespace TheatricalPlayers.Core.Interfaces.Statements;

public interface IStatementPrinterHandler
{
    public string PrintTxt(Invoice invoice, List<Play> plays);
    public string PrintXml(Invoice invoice, List<Play> plays);
    public Statement GetStatement(Invoice invoice, List<Play> plays);
}