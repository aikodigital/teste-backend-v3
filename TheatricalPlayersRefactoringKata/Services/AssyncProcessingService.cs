using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class AsyncProcessingService
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public AsyncProcessingService(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public async Task ProcessAsync(Invoice invoice, Dictionary<string, Play> plays, string filePath)
        {
            var asyncProcessor = new AsyncStatementProcessor(_calculatorFactory);
            await asyncProcessor.GenerateXmlAsync(invoice, plays, filePath);
        }
    }
}
