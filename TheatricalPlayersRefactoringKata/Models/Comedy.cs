using System;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Comedy : IPlay
    {
        public string Name { get; }
        public int Lines { get; }

        public Comedy(string name, int lines)
        {
            Name = name;
            Lines = lines;
        }

        public decimal CalculateAmount(int audience)
        {
            decimal amount = 30000; // Base amount
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            amount += 300 * audience;
            return amount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return Math.Max(audience - 30, 0) + (int)Math.Floor((decimal)audience / 5);
        }
    }
}
