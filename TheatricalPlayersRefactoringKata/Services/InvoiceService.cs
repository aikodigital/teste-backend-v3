using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class InvoiceService
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public InvoiceService(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public void ProcessInvoice(Invoice invoice, Dictionary<string, Play> plays)
        {
            var printer = new XmlStatementPrinter(_calculatorFactory);
            var result = printer.Print(invoice, plays);

            // Faça algo com o resultado, como salvar em arquivo ou retornar para a chamada
        }
    }
}
