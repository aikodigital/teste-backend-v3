namespace TheatricalPlayersRefactoringKata.Modules
{
    public class History : AbstractPlayType
    {
        override public decimal CalculateAmount(Performance performance, Play play)
        {
            decimal comedyAmount = new Comedy().CalculateAmount(performance, play);
            decimal tragedyAmount = new Tragedy().CalculateAmount(performance, play);
            return comedyAmount + tragedyAmount;
        }
    }
}