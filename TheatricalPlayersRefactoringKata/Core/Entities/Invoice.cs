namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Invoice(List<Performance> performance,string customer, Guid id = new Guid())
{

    public Invoice() : this([], string.Empty)
    {
    }
    public Guid Id { get; } = id;
    public string Customer { get; } = customer;

    public List<Performance> Performances { get; } = performance;
}