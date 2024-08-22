using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class AsyncProcessingService
    {
        private readonly ConcurrentQueue<(Invoice, Dictionary<string, Play>, string)> _queue;
        private readonly AsyncStatementProcessor _asyncStatementProcessor;

        public AsyncProcessingService()
        {
            _queue = new ConcurrentQueue<(Invoice, Dictionary<string, Play>, string)>();
            _asyncStatementProcessor = new AsyncStatementProcessor();
        }

        public void EnqueueInvoice(Invoice invoice, Dictionary<string, Play> plays, string filePath)
        {
            _queue.Enqueue((invoice, plays, filePath));
        }

        public async Task ProcessQueueAsync()
        {
            while (_queue.TryDequeue(out var item))
            {
                await _asyncStatementProcessor.GenerateXmlAsync(item.Item1, item.Item2, item.Item3);
            }
        }
    }
}
