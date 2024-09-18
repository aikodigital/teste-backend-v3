using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.UseCases;

public class EnqueueStatementUseCase
{
    private readonly IStatementQueue _statementQueue;

    public EnqueueStatementUseCase(IStatementQueue statementQueue)
    {
        _statementQueue = statementQueue;
    }

    public void Execute(Invoice invoice)
    {
        _statementQueue.EnqueueStatement(invoice);
    }
}
