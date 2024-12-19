using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.API.Data
{
    public class InvoiceRecordEntity
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string StatementXml { get; set; }
        public DateTime ProcessedAt { get; set; }

        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
