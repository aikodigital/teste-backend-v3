using System.Collections.Generic;
using System.Linq;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances
    {
        get => _performances;
        set
        {
            _performances = value;

        }
    }


    public decimal CalculateTotals()
    {
        return _performances != null ? _performances.Sum(perf => perf.CalculateValue()) : 0;
    }
    public int CalculateCredits()
    {
        return _performances != null ? _performances.Sum(perf => perf.CalculateCredits()) : 0;
    }

    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }

}
