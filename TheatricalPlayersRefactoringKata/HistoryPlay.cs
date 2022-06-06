namespace TheatricalPlayersRefactoringKata
{
    public class HistoryPlay : Play
    {
        public HistoryPlay(string name, int lines) : base(name, lines)
        {
        }

        public override decimal CalculateBaseValue(int audience)
        {
            return 0;
        }

        protected override int CalculateCredits(int audience)
        {
            return 0;
        }
    }
}
