    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface ITypeCalculatorFactory
    {
        ITypeGenericCalculator GetCalculator(string playType);
    }
}
