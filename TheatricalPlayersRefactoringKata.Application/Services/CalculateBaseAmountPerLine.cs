using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Constants;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class CalculateBaseAmountPerLine : ICalculateBaseAmountPerLine
    {
        public decimal CalculateBaseAmount(int lines)
        {
            if (lines < StatementPrinterConstants.MINIMUM_LINES)
                lines = StatementPrinterConstants.MINIMUM_LINES;
            if (lines > StatementPrinterConstants.MAXIMUM_LINES)
                lines = StatementPrinterConstants.MAXIMUM_LINES;

            return lines / StatementPrinterConstants.DIVIDER_PER_LINE;
        }
    }
}
