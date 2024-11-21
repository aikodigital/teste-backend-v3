using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Utilities
{
    public static class CreditHelper
    {
        public static int CalculateBaseCredit(int audienceSize)
        {
            //Todas performances dão 1 crédito para cada espectador acima de 30, não valendo nenhum crédito para uma platéia menor ou igual a 30
            return Math.Max(0, audienceSize - 30);
        }
    }
}
