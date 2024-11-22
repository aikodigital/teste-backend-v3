namespace Aplication.Services.Calculators
{
    public class ComedyCalculator : PriceCalculator
    {
        public override int CalculatePrice(int audience)
        {
            int price = 300 * audience;
            if (audience > 20) price += 10000 + 500 * (audience - 20);
            return price;
        }
    }
}
