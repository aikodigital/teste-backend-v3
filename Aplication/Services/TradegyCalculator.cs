using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class TragedyCalculator : PriceCalculator
    {
        public override int CalculatePrice(int audience)
        {
            int price = 0;
            if (audience > 30) price += 1000 * (audience - 30);
            return price;
        }
    }
}
