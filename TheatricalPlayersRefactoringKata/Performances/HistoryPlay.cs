namespace TheatricalPlayersRefactoringKata.Performances
{
    public class HistoryPlay : Play
    {
        private const int HISTORY_TRAGEDY_ADICIONAL_AUDIENCE_VALUE = 10;
        private const int HISTORY_TRAGEDY_MAX_AUDIENCE = 30;

        private const int HISTORY_COMEDY_DEFAULT_AUDIENCE_VALUE = 3;
        private const int HISTORY_COMEDY_ADICIONAL_AUDIENCE_VALUE = 5;
        private const int HISTORY_COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED = 100;
        private const int HISTORY_COMEDY_MAX_AUDIENCE = 20;

        public HistoryPlay(string name, int lines) : base(name, lines)
        {
        }

        public override void CalculateBaseValue(int audience)
        {
            SumBaseValue(BaseValue + HISTORY_COMEDY_DEFAULT_AUDIENCE_VALUE * audience);

            if (audience > HISTORY_TRAGEDY_MAX_AUDIENCE)
                SumBaseValue(HISTORY_TRAGEDY_ADICIONAL_AUDIENCE_VALUE * (audience - HISTORY_TRAGEDY_MAX_AUDIENCE));

            if (audience > HISTORY_COMEDY_MAX_AUDIENCE)
            {
                SumBaseValue(HISTORY_COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED +
                             HISTORY_COMEDY_ADICIONAL_AUDIENCE_VALUE * (audience - HISTORY_COMEDY_MAX_AUDIENCE));
            }
        }

        protected override int CalculateCredits(int audience)
        {
            return 0;
        }
    }
}
