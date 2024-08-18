using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    private Customer _customer;
    private List<Performance> _performances;

    public Customer Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(Customer customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }
}
