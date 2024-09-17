using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class InvoiceModelView
{
    private string _customer;
    private List<PerformanceModelView> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<PerformanceModelView> Performances { get => _performances; set => _performances = value; }

    public InvoiceModelView(string customer, List<PerformanceModelView> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }

}
