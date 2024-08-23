using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Processing
{
    public class DataQueue
    {
        private readonly Queue<Play> _queue = new Queue<Play>();

        public void Enqueue(IEnumerable<Play> plays)
        {
            foreach (var play in plays)
            {
                _queue.Enqueue(play);
            }
        }

        public async Task ProcessQueueAsync()
        {
            await Task.Run(() =>
            {
                while (_queue.Count > 0)
                {
                    var play = _queue.Dequeue();
                    System.Console.WriteLine($"Processing: {play.Title}");
                }
            });
        }
    }
}
