using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class CalculateService : ICalculateService
    {
        public CalculateService()
        {

        }

        private decimal CalculateBaseValue(int lines)
        {
            if (lines < 1000)
                lines = 1000;

            if (lines > 4000)
                lines = 4000;

            decimal baseValue = lines / 10;

            return baseValue;
        }

        private decimal CalculateTragedyValue(int audience)
        {
            decimal baseValue = CalculateBaseValue(audience);
            const int baseAudience = 30;
            if (audience > baseAudience)
            {
                int audienceGreatherThan = (audience - baseAudience);
                baseValue += (audienceGreatherThan * 10.00m);
            }

            decimal tragedyValue = baseValue;

            return tragedyValue;
        }

        private decimal CalculateComedyValue(int audience)
        {
            decimal baseValue = CalculateBaseValue(audience);
            const int baseAudience = 20;

            baseValue += (audience * 3.00m);

            if (baseAudience > 20)
            {
                baseValue += 100.00m;

                int audienceGreatherThan = (audience - baseAudience);
                baseValue += (audienceGreatherThan * 10.00m);
            }

            decimal comedyValue = baseValue;

            return comedyValue;
        }

        public decimal CalculateValueByType(PlayTypeEnum playTypeEnum, int audience)
        {
            if (playTypeEnum == PlayTypeEnum.Tragedy)
                return CalculateTragedyValue(audience);

            if (playTypeEnum == PlayTypeEnum.Comedy)
                return CalculateComedyValue(audience);

            if (playTypeEnum == PlayTypeEnum.History)
            {
                return CalculateTragedyValue(audience) + CalculateComedyValue(audience);
            }

            return 0;
        }

        public decimal CalculateCreditsByType(PlayTypeEnum playTypeEnum, int audience)
        {
            decimal baseCredits = CalculateBaseCredits(audience);

            if (playTypeEnum == PlayTypeEnum.Comedy)
                baseCredits += CalculateBonusCreditsComedy(audience);

            return baseCredits;
        }

        private decimal CalculateBaseCredits(int audience)
        {
            decimal credits = 0;

            if (audience > 30)
            {
                credits = (audience * 1.00m);
            }

            return credits;
        }

        private decimal CalculateBonusCreditsComedy(int audience)
        {
            decimal credits = Math.Floor(audience / 5.00m);

            return credits;
        }
    }
}