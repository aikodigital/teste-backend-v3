namespace TheatricalPlayersRefactoringKata.Contracts;

public interface IPerformance
{
    int Audience { get; set; }
    IPlay Play { get; set; }

    decimal CalculateAmmount(decimal baseAmount);
    int CalculateCredits();
}