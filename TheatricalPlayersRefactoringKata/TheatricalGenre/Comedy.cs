namespace TheatricalPlayersRefactoringKata.TheatricalGenre
{
    public class Comedy : Genre
    {
        public override int CalculateAmount(int audience, int lines)
        {
            int amount = base.CalculateAmount(audience, lines);
            if (audience > 20) amount += 10000 + 500 * (audience - 20);
            amount += 300 * audience;
            return amount;
        }

        public override int CalculateVolumeCredits(int audience)
        {
            return base.CalculateVolumeCredits(audience) + (int)System.Math.Floor((decimal)audience / 5);
        }
    }
}