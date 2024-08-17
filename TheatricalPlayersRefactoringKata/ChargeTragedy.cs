using System;
using static TheatricalPlayersRefactoringKata.StatementPrinter;

namespace TheatricalPlayersRefactoringKata
{   
    //???
    public class CobrancaTragedia : IChargeStrategy
    {

        public int CalcularCobranca(Performance performance, Play play)
        {
            // limites de linhas
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            // a base da cobrança são as linhas x10
            var amount = lines * 10;

            // adiciona valor se a audiência for superior a 30
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }

            return amount;
        }

        public int CalcularCreditos(Performance performance)
        {
            // crédito para audiência maior que 30
            return Math.Max(performance.Audience - 30, 0);
        }

    }
}