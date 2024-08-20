namespace TheatricalPlayersRefactoringKata.Modules;

public class PerformanceResults
{
    public decimal AmountOwed { get; set; }
    public decimal EarnedCredits { get; set; }
    public Play? Play { get; set; }
}