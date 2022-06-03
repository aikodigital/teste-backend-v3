namespace TheatricalPlayersRefactoringKata
{
    public class ComedyPlay : Play
    {
        private const int COMEDY_DEFAULT_AUDIENCE_VALUE = 3;
        private const int COMEDY_ADICIONAL_AUDIENCE_VALUE = 5;
        private const int COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED = 100;
        private const int COMEDY_MAX_AUDIENCE = 20;

        public ComedyPlay(string name, int lines) : base(name, lines)
        {
        }

        public override int CalculateBaseValue(Performance performance)
        {
            

            if (performance.Audience > COMEDY_MAX_AUDIENCE)
            {
               SumBaseValue(COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED +
                             COMEDY_ADICIONAL_AUDIENCE_VALUE * (performance.Audience - COMEDY_MAX_AUDIENCE));
            }

            SumBaseValue(COMEDY_DEFAULT_AUDIENCE_VALUE * performance.Audience);


            return BaseValue;
        }
    }
}
