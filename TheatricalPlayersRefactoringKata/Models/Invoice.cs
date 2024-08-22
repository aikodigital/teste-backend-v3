using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    private Customer _customer;
    private List<Performance> _performances;

    public Customer Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    public Invoice(Customer customer, List<Performance> performances)
    {
        _customer = customer is not null ? customer : throw new ArgumentException("Customer cannot be null");
        _performances = performances is not null ? performances : throw new ArgumentException("Performance list cannot be null");
    }
}
