﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Calculators.Interfaces;
using TheatricalPlayersRefactoringKata.Calculators.Types;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public class TypeCalculatorFactory : ITypeCalculatorFactory
    {
        public ITypeCalculator GetCalculator(string playType)
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
