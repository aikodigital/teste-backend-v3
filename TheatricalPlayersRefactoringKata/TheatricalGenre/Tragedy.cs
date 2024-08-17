namespace TheatricalPlayersRefactoringKata.TheatricalGenre
{
    public class Tragedy : Genre
    {
        public override int CalculateAmount(int audience, int lines)
        {
            int amount = base.CalculateAmount(audience, lines);
            if (audience > 30) amount += 1000 * (audience - 30);
            return amount;
        }
    }
}