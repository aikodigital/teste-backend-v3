using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    public string Customer { get; set; }
    public IReadOnlyList<Performance> Performances { get; }

    public Invoice(string customer, List<Performance> performances)
    {
        Customer = customer;
        Performances = performances.AsReadOnly();
    }
}
