using System.Collections.Generic;
using System.Linq;

namespace TheatricalPlayersRefactoringKata;


//Fatura
public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer => _customer;
    public IReadOnlyList<Performance> Performances => _performances.AsReadOnly();
    public decimal TotalAmount => _performances.Sum(p => p.Play.BaseValue);
    public int VolumeCredits => _performances.Sum(p => p.GetCredits());

    public Invoice(string customer, List<Performance> performance)
    {
        _customer = customer;
        _performances = performance;
    }

    public void Calculute()
    {
        _performances.ForEach(p => p.Play.CalculateBaseValue(p));
    }

    public Performance GetPerformanceByName(string name)
    {
        return _performances.SingleOrDefault(p => p.Play.Name == name);
    }

}
