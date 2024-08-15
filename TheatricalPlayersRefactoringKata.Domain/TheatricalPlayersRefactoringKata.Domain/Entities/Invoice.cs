using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Invoice
{
    private long _id;
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performance, long id)
    {
        _customer = customer;
        _performances = performance;
        _id = id;
    }

}
