using TheatricalPlayersRefactoringKata;
using Xunit;

public class TragedyCalculator
{
    public decimal CalculateAmount(Performance performance, Play play)
    {
        if (play.Type == "tragedy")
        {
            var baseAmount = 400m;
            var additionalAmount = 100m;

            var audience = performance.Audience;
            if (audience > 30)
            {
                baseAmount += additionalAmount * (audience - 30);
            }

            return baseAmount;
        }

        // Outras lógicas para diferentes tipos de peças
        return 0m;
    }

    public int CalculateCredits(Performance performance, Play play)
    {
        // Lógica para cálculo de créditos
        return 0;
    }
}
