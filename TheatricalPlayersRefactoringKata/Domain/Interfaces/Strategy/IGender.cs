namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Strategy
{
    public interface IGender
    {
        decimal Calculate(decimal basePrice, int audience);
    }
}
