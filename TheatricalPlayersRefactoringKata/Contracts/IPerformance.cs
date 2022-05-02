namespace TheatricalPlayersRefactoringKata.Contracts;

public interface IPerformance
{
    int Audience { get; set; }
    IPlay Play { get; set; }

    int CalculateAmmount();
    int CalculateCredits();
}