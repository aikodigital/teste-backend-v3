using TheatricalPlayers.Core.Entities;

namespace TheatricalPlayers.Core.Interfaces.Statements;

public interface IStatementPrinter
{
    public string Print(Invoice invoice, List<Play> plays);
}