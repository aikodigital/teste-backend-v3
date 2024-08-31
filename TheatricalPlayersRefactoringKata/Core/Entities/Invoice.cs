using System.Collections.Generic;
using System;

namespace TheatricalPlayersRefactoringKata.Core.Entities;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Customer { get; set; } = string.Empty;
    public IList<Performance> Performances { get; set; } = new List<Performance>();

    public Invoice(string customer, List<Performance> performances, Guid id = new())
    {
        Performances = performances;
        Customer = customer;
        Id = id;

    }
}