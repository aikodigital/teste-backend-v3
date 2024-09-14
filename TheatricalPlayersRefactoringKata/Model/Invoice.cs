using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Model;

public class Invoice
{
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; }

    public Invoice()
    {

    }
    public Invoice(string customer, List<Performance> performance)
    {
        Customer = customer;
        Performances = performance;
    }
}
