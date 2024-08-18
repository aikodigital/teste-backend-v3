using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class InvoiceModel
{
    private string _customer;
    private List<PerformanceModel> _performances;

    public string Customer { get => _customer; private set => _customer = value; }
    public List<PerformanceModel> Performances { get => _performances; private set => _performances = value; }

    public InvoiceModel(string customer, List<PerformanceModel> performance)
    {
        _customer = customer;
        _performances = performance;
    }

}
