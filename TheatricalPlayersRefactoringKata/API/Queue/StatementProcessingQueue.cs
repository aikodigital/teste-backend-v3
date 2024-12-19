using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.API.Queue
{
    public class StatementProcessingQueue : ITheaterStatementProcessingQueue 
    {
        private readonly ConcurrentQueue<string> _queue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public Task EnqueueAsync(string invoiceJson)
        {
            _queue.Enqueue(invoiceJson);
            _signal.Release();
            return Task.CompletedTask;
        }

        public async Task<string> DequeueAsync()
        {
            await _signal.WaitAsync();
            _queue.TryDequeue(out var invoiceJson);
            return invoiceJson;
        }
    }
}
