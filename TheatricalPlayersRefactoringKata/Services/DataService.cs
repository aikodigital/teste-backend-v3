using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Processing;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class DataService
    {
        private readonly DataQueue _dataQueue;

        public DataService(DataQueue dataQueue)
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
