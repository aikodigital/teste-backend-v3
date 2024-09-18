using System;

namespace TheatricalPlayersRefactoringKata.Domain
{
    public class ComedyPlay :PlayTemplate
    {
        public ComedyPlay(string name) : base(name, "comedy") { }

        public override decimal CalculateAmount(int audience, int lines)
        {
            lines = Math.Max(lines, 1000);  // Limite inferior
            lines = Math.Min(lines, 4000);  // Limite superior

            var amount = (lines * 10m) + 300m * audience; // Valor base + por pessoa
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            return amount;
        }

        public override int CalculateVolumeCredits(int audience)
        {
            var credits = Math.Max(audience - 30, 0);
            if (audience > 30)
                credits += (int)Math.Floor((decimal)audience / 5);
                //Mais Créditos para comédias com mais audiência
            return credits;
        }
    }
}
