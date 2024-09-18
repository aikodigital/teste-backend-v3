using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IStatementQueue
{
    void EnqueueStatement(Invoice invoice);
    bool TryDequeueStatement(out Invoice invoice);
}
