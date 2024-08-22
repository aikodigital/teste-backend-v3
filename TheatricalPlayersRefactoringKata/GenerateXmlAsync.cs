using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class SomeService
    {
        private readonly AsyncStatementProcessor _asyncStatementProcessor;

        public SomeService()
        {
            _asyncStatementProcessor = new AsyncStatementProcessor();
        }

        public async Task GenerateInvoiceXmlAsync(Invoice invoice, Dictionary<string, Play> plays, string filePath)
        {
            await _asyncStatementProcessor.GenerateXmlAsync(invoice, plays, filePath);
        }
    }
}
