using System;
using System.Collections.Generic;

namespace TP.Domain.Entities;

public class Invoice
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public List<Performance> Performances { get; set; }

    public Invoice(string customerName, List<Performance> performances)
    {
        CustomerName = customerName;
        Performances = performances;
    }
}