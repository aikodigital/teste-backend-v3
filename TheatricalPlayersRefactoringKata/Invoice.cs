using System.Collections.Generic;
namespace TheatricalPlayersRefactoringKata;
public class Invoice
{
    public string Customer { get; }
    public List<Performance> Performances { get; }

    /// <summary>Represents an invoice for a series of theatrical performances.</summary>
    /// <param name="customer">Customer name</param>
    /// <param name="performance"> List of performances</param>
    public Invoice(string customer, List<Performance> performance)
    {
        Customer = customer;
        Performances = performance;
    }
}