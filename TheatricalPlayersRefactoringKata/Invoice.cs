using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; private set => _customer = value; }
    public List<Performance> Performances { get => _performances; private set => _performances = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        Customer = customer;
        Performances = performance;
    }

}
