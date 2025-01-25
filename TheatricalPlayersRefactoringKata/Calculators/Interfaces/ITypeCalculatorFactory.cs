using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Calculators.Interfaces
{
    public interface ITypeCalculatorFactory
    {
        ITypeCalculator GetCalculator(string playType);
    }
}
