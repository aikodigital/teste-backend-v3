using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class SomeService
    {
        private readonly ICalculatorFactory _calculatorFactory;

        public SomeService(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public void Process()
        {
            var asyncProcessor = new AsyncStatementProcessor(_calculatorFactory);
            // Continue com a lógica
        }
    }
}
