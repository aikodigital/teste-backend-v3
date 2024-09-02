using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Invoice
{
    public int Id { get; set; }
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; }

    public Invoice() { }

    public Invoice(string customer, List<Performance> performance)
    {
        Customer = customer;
        Performances = performance;
    }
}
