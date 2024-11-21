namespace Aplication.Services
{
    public class HistoryCalculator : PriceCalculator
    {
        private readonly TragedyCalculator _tragedyCalculator = new();
        private readonly ComedyCalculator _comedyCalculator = new();

        public override int CalculatePrice(int audience)
        {
            return _tragedyCalculator.CalculatePrice(audience) +
                   _comedyCalculator.CalculatePrice(audience);
        }
    }
}
