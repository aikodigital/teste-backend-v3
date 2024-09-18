using TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy;

namespace TheatricalPlayersRefactoringKata.Domain.Entities.Gender
{
    public class History : IGender
    {
        private readonly Tragedy _tragedy;
        private readonly Comedy _comedy;

        public History(Tragedy tragedy, Comedy comedy)
        {
            _tragedy = tragedy;
            _comedy = comedy;
        }

        public decimal Calculate(decimal basePrice, int audience)
        {
            var comedy = _comedy.Calculate(basePrice, audience);
            var tragedy = _tragedy.Calculate(basePrice, audience);

            return comedy + tragedy;
        }
    }
}
