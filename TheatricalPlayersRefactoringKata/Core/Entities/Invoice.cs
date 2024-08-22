namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Invoice(string customer, List<Performance> performance)
{
    public string Customer { get; } = customer;

    public List<Performance> Performances { get; } = performance;
}