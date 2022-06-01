using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;


//Fatura
public class Invoice
{
    private string _customer;
    private List<Performance> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }

    //Todo
    public decimal TotalAmount { get; set; }
    public int VolumeCredits { get; set; }

    public Dictionary<string, decimal> PerformancesAmountCurtumer { get; set; } =
        new Dictionary<string, decimal>();


    public Invoice(string customer, List<Performance> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }

}
