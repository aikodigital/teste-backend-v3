using System.Collections.Concurrent;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Queues;

public class StatementQueue : IStatementQueue
{
    private readonly ConcurrentQueue<Invoice> _queue = new ConcurrentQueue<Invoice>();

    public void EnqueueStatement(Invoice invoice)
    {
        _queue.Enqueue(invoice);
    }

    public bool TryDequeueStatement(out Invoice invoice)
    {
        return _queue.TryDequeue(out invoice);
    }
}
