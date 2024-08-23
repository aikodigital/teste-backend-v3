using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Processing;

namespace TheatricalPlayersRefactoringKata.Processing
{
    public class DataProcessor
    {
        private readonly DataQueue _dataQueue;

        public DataProcessor(DataQueue dataQueue)
        {
            _dataQueue = dataQueue;
        }

        public async Task ProcessDataAsync(IEnumerable<Play> plays)
        {
            _dataQueue.Enqueue(plays);
            await _dataQueue.ProcessQueueAsync();
        }
    }
}
