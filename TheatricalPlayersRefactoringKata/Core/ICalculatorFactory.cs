using TheatricalPlayersRefactoringKata.Calculators;

namespace TheatricalPlayersRefactoringKata
{
    public interface ICalculatorFactory
    {
        ICalculator GetCalculator(string type);
    }
}
