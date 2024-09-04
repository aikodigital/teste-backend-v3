using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Invoice
{
    public int Id { get; set; }
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice()
    {
    }

    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }
}
