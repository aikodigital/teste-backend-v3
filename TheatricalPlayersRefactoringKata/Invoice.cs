using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class Invoice
{
    public string Customer { get; private set; }
    public List<Performance> Performances { get; private set; }

    public Invoice(string customer, List<Performance> performances)
    {
        Customer = customer;
        Performances = performances;
    }

    public decimal TotalAmount()
    {
        decimal totalAmount = 0;
        foreach (var performance in Performances)
        {
            totalAmount += performance.CalculateAmount();
        }
        return totalAmount;
    }

    public int TotalCredits()
    {
        int totalCredits = 0;
        foreach (var performance in Performances)
        {
            totalCredits += performance.CalculateCredits();
        }
        return totalCredits;
    }
}
