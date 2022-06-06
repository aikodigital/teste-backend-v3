using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata;

public class Invoice
{
    public string Customer { get; set; }
    public List<IPerformance> Performances { get; init; }

    public Invoice(string customer, List<IPerformance> performances)
    {
        Customer = customer;
        Performances = performances;
    }
}
