using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TheatricalPlayersRefactoringKata;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }

    public List<Performance> Performances
    {
        get => _performances;
        set => _performances = value;
    }
    public int TotalCredits => Performances?.Sum(performance => performance.Credits) ?? 0;
    public decimal TotalCosts => Performances?.Sum(performance => performance.Cost) ?? 0;
    public Invoice(string customer, List<Performance> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }

}
