using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Enums;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services.InvoicePrice
{
    public static class PerformancePrice
    {
        public static int Price(int lines, int audience, PlayType playType)
        {
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;
            var thisAmount = lines * 10;

            switch (playType)
            {
                case PlayType.Tragedy:
                    thisAmount = TragedyUseCase(thisAmount, audience);
                    break;
                case PlayType.Comedy:
                    thisAmount = ComedyUseCase(thisAmount, audience);
                    break;
                case PlayType.History:
                    thisAmount = HistoryUseCase(thisAmount, audience);
                    break;
                default:
                    throw new Exception("unknown type: " + playType);
            }

            return 0;
        }

        private static int TragedyUseCase(int thisAmount, int audience)
        {
            if (audience > 30) return thisAmount += 1000 * (audience - 30);
            return thisAmount;
        }

        private static int ComedyUseCase(int thisAmount, int audience)
        {
            if (audience > 20) thisAmount += 10000 + 500 * (audience - 20);
            thisAmount += 300 * audience;
            return thisAmount;
        }

        private static int HistoryUseCase(int thisAmount, int audience)
        {
            var tragedyValue = thisAmount;
            var comedyValue = thisAmount;

            if (audience > 30) tragedyValue += 1000 * (audience - 30);
            if (audience > 20) comedyValue += 10000 + 500 * (audience - 20);
            comedyValue += 300 * audience;

            thisAmount = tragedyValue + comedyValue;
            return thisAmount;
        }
    }
}
