using System.Collections.Generic;
using System.Linq;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Invoice
{
    public string Customer { get; private set; }
    public List<Performance> Performances { get; private set; }

    public Invoice(string customer, List<Performance> performances)
    {
        Customer = customer;
        Performances = performances;
    }

}
