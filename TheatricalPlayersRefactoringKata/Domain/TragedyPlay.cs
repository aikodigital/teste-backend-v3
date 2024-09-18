using System;

namespace TheatricalPlayersRefactoringKata.Domain
{
    public  class TragedyPlay : PlayTemplate
    {
        public TragedyPlay(string name) : base(name, "tragedy") { }

        public override decimal CalculateAmount(int audience, int lines)
        {
            lines = Math.Max(lines, 1000);  // Limite inferior
            lines = Math.Min(lines, 4000);  // Limite superior

            var amount = lines * 10m; // Valor base para tragédias
            if (audience > 30)
            {
                amount += 1000m * (audience - 30);
            }
            return amount;
        }

        public override int CalculateVolumeCredits(int audience)
        {
            return audience > 30 ? audience - 30 : 0;
        }
    }
}
