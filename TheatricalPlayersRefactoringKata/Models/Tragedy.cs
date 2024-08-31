using System;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Tragedy : IPlay
    {
        public string Name { get; }
        public int Lines { get; }

        public Tragedy(string name, int lines)
        {
            Name = name;
            Lines = lines;
        }

        public decimal CalculateAmount(int audience)
        {
            var baseAmount = Math.Max(Lines / 10, 100);
            if (audience > 30)
            {
                return baseAmount + 10 * (audience - 30);
            }
            return baseAmount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return Math.Max(audience - 30, 0);
        }
    }
}
