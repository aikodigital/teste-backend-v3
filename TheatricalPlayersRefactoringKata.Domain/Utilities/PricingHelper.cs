using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Utilities
{
    public static class PricingHelper
    {
        public static decimal CalculateBasePrice(int lines)
        {
            //O número de linhas da peça considerado para o cálculo do valor base deve ser forçado a estar no intervalo entre 1000 e 4000
            int constrainedLines = Math.Clamp(lines, 1000, 4000);
            return constrainedLines / 10.0m;
        }
    }
}
