namespace Aplication.Services.Calculators
{
    public class HistoryCalculator : PriceCalculator
    {
        private readonly TragedyCalculator _tragedyCalculator = new();
        private readonly ComedyCalculator _comedyCalculator = new();

        public override int CalculatePrice(int audience)
        {
            return ReservedValue + _tragedyCalculator.CalculatePrice(audience) +
                   _comedyCalculator.CalculatePrice(audience);
        }
    }
}
