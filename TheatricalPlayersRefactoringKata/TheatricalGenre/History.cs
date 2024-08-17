namespace TheatricalPlayersRefactoringKata.TheatricalGenre
{
    public class History : Genre
    {
        private readonly Comedy _comedy = new Comedy();
        private readonly Tragedy _tragedy = new Tragedy();
        public override int CalculateAmount(int audience, int lines)
        {
            int comedyAmount = _comedy.CalculateAmount(audience, lines);
            int tragedyAmount = _tragedy.CalculateAmount(audience, lines);
            return comedyAmount + tragedyAmount;
        }
    }
}