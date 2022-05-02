namespace TheatricalPlayersRefactoringKata.Contracts;

public interface IPerformance
{
    string PlayId { get; set; }
    int Audience { get; set; }
    IPlay Play { get; set; }

    int CalculateAmmount();
    int CalculateCredits();
}