namespace Main.Domain.Services
{
    public class StatementCalculator
    {
        public int CalculateTragedyAmount(int amount,int audience)
        {
            if (audience > 30)
            {
                amount += 1000 * (audience - 30);
            }
            return amount;
        }        
        
        public int CalculateComedyAmount(int amount,int audience)
        {
            amount += 300 * audience;
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            return amount;
        }
        public int CalculateHistoryAmount(int amount,int audience)
        {
            return CalculateComedyAmount(amount, audience) + CalculateTragedyAmount(amount,audience);
        }

        public int CalculateVolumeCredits(int volumeCredits,string type,int audience)
        {
            volumeCredits += Math.Max(audience - 30, 0);

            if(type == "comedy")
            {
                volumeCredits += (int)Math.Floor((decimal)audience / 5);
            }

            return volumeCredits;
        }

        public int AdjustLines(int lines)
        {
            if (lines < 1000) return 1000;
            if (lines > 4000) return 4000;
            return lines;
        }
    }
}
