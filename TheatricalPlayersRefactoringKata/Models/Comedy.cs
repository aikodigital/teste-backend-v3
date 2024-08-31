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
            var baseAmount = Math.Max(Lines / 10, 100);
            var thisAmount = baseAmount + 3 * audience;
            if (audience > 20)
            {
                thisAmount += 100 + 5 * (audience - 20);
            }
            return thisAmount;
        }

        public int CalculateVolumeCredits(int audience)
        {
            return Math.Max(audience - 30, 0) + (int)Math.Floor((decimal)audience / 5);
        }
    }
}
