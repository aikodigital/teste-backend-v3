namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Invoice(List<Performance> performance, string customer, Guid id = new())
{
    public Invoice() : this([], string.Empty)
    {
    }

    public Guid Id { get; init; } = id;
    public string Customer { get; init; } = customer;

    public List<Performance> Performances { get; set; } = performance;
}