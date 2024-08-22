namespace TheatricalPlayersRefactoringKata.Domain.Entity;

public class Invoice
{
    public Guid Id { get; private set; }
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; }

    public Invoice(string customer, List<Performance> performance)
    {
        Id = new Guid();
        Customer = customer;
        Performances = performance;
    }
}
