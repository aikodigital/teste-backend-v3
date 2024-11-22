namespace Aplication.Services.Calculators
{
    public abstract class PriceCalculator
    {
        public int ReservedValue { get; set; }
        public abstract int CalculatePrice(int audience);
    }
}
