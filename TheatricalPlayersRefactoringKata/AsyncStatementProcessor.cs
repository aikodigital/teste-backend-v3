using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata
{
    public class AsyncStatementProcessor
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public AsyncStatementProcessor(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public async Task GenerateXmlAsync(Invoice invoice, Dictionary<string, Play> plays, string filePath)
        {
            var printer = new XmlStatementPrinter(_calculatorFactory);
            var result = printer.Print(invoice, plays);

            await File.WriteAllTextAsync(filePath, result);
        }
    }
}
