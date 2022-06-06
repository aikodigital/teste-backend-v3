using System;

namespace TheatricalPlayersRefactoringKata.Performances
{
    public class ComedyPlay : Play
    {
        private const int COMEDY_DEFAULT_AUDIENCE_VALUE = 3;
        private const int COMEDY_ADICIONAL_AUDIENCE_VALUE = 5;
        private const int COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED = 100;
        private const int COMEDY_MAX_AUDIENCE = 20;
        private const int COMEDY_AUDIENCE_DIVISION_CREDIT = 5;

        public ComedyPlay(string name, int lines) : base(name, lines)
        {
        }

        public override void CalculateBaseValue(int audience)
        {
            if (audience > COMEDY_MAX_AUDIENCE)
            {
                SumBaseValue(COMEDY_ADICIONAL_AUDIENCE_VALUE_INCREASED +
                              COMEDY_ADICIONAL_AUDIENCE_VALUE * (audience - COMEDY_MAX_AUDIENCE));
            }

            SumBaseValue(COMEDY_DEFAULT_AUDIENCE_VALUE * audience);
        }

        protected override int CalculateCredits(int audience)
        {
            return (int)Math.Floor((decimal)audience / COMEDY_AUDIENCE_DIVISION_CREDIT);
        }
    }
}
