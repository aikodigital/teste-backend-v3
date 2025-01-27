using System;
using TheatricalPlayersRefactoringKata.Domain.Calculators.Types;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.Calculators
{
    public class TypeCalculatorFactory : ITypeCalculatorFactory
    {
        public ITypeGenericCalculator GetCalculator(string playType)
        {
            return playType switch
            {
                "tragedy" => new TragedyCalculator(),
                "comedy" => new ComedyCalculator(),
                "history" => new HistoryCalculator(),
                _ => throw new NotImplementedException("Nao implementado o tipo:" + playType)

            };
        }
    }
}
