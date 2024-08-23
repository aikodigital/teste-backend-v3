using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Invoice
{
    public Invoice(List<Performance> performance, string customer, Guid id = new ())
    {
        Customer = customer;
        Performances = performance;
        Id = id;
    }

    public Invoice()
    {
    }

    public Guid Id { get; init; }
    
    [MaxLength(30)]
    public string Customer { get; init; } = string.Empty;

    public List<Performance> Performances { get; set; } = new();
}