using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class CalculateService : ICalculateService
    {
        public CalculateService()
        {

        }

        public decimal CalculateBaseValue(int lines)
        {
            if (lines < 1000)
                lines = 1000;

            if (lines > 4000)
                lines = 4000;

            decimal baseValue = lines / 10;

            return baseValue;
        }

        public decimal CalculateTragedyValue(int audience, int lines)
        {
            if (audience <= 0)
                return 0;

            if (lines <= 0)
                return 0;

            decimal baseValue = CalculateBaseValue(lines);
            const int baseAudience = 30;
            if (audience > baseAudience)
            {
                baseValue += ((audience - baseAudience ) * 10.00m);
            }

            decimal tragedyValue = baseValue;

            return tragedyValue;
        }

        public decimal CalculateComedyValue(int audience, int lines)
        {
            if (audience < 0)
                audience = 0;

            decimal baseValue = CalculateBaseValue(lines);
            const int baseAudience = 20;

            baseValue += (audience * 3.00m);

            if (baseAudience > 20)
            {
                baseValue += 100.00m;

                baseValue += ((audience - baseAudience ) * 10.00m);
            }

            decimal comedyValue = baseValue;

            return comedyValue;
        }

        public decimal CalculateHistoryValue(int audience, int lines)
        {
            decimal tragedy = CalculateTragedyValue(audience, lines);
            decimal comedyValue = CalculateComedyValue(audience, lines);

            return (tragedy + comedyValue);
        }

        public decimal CalculateValueByType(PlayTypeEnum playTypeEnum, int audience, int lines)
        {
            if (playTypeEnum == PlayTypeEnum.Tragedy)
                return CalculateTragedyValue(audience, lines);

            if (playTypeEnum == PlayTypeEnum.Comedy)
                return CalculateComedyValue(audience, lines);

            if (playTypeEnum == PlayTypeEnum.History)
            {
                return CalculateHistoryValue(audience, lines);
            }

            return 0;
        }

        public int CalculateCreditsByType(PlayTypeEnum playTypeEnum, int audience)
        {
            int baseCredits = CalculateBaseCredits(audience);

            if (playTypeEnum == PlayTypeEnum.Comedy)
                baseCredits += CalculateBonusCreditsComedy(audience);

            return baseCredits;
        }

        public int CalculateBaseCredits(int audience)
        {
            int credits = 0;

            const int baseAudience = 30;
            if (audience > 30)
            {
                credits = ((audience - baseAudience ) * 1);
            }

            return credits;
        }

        public int CalculateBonusCreditsComedy(int audience)
        {
            if (audience <= 0)
                audience = 0;

            int credits = (int)Math.Floor(audience / 5m);

            return credits;
        }
    }
}