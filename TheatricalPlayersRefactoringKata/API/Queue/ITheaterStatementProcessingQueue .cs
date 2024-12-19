using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.API.Queue
{
    public interface ITheaterStatementProcessingQueue 
    {
        Task EnqueueAsync(string invoiceJson);
        Task<string> DequeueAsync();
    }
}
