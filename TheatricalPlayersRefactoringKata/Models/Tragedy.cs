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
            decimal amount = 40000; // Base amount
            if (audience > 30)
            {
                amount += 1000 * (audience - 30);
            }
            return amount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return Math.Max(audience - 30, 0);
        }
    }
}
