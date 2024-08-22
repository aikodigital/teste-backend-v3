using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models;

public class Invoice
{
    public Customer Customer { get; set; }
    public List<Performance> PerformanceList { get; set; }

    public Invoice(Customer customer, List<Performance> performances)
    {
        Customer = customer is not null ? customer : throw new ArgumentException("Customer cannot be null");
        PerformanceList = performances is not null ? performances : throw new ArgumentException("Performance list cannot be null");
    }
}
