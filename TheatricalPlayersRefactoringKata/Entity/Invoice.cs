using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entity;

public class Invoice
{

    public Invoice() { }

    public Guid Id{ get; set; }

    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }

}
