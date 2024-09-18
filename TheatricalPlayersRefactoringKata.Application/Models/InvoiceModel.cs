namespace TheatricalPlayersRefactoringKata.Application.Models;

public class InvoiceModel
{
    private string _customer;
    private List<PerformanceModel> _performances;

    public string Customer { get => _customer; set => _customer = value; }
    public List<PerformanceModel> Performances { get => _performances; set => _performances = value; }

    public InvoiceModel(string customer, List<PerformanceModel> performance)
    {
        _customer = customer;
        _performances = performance;
    }

}
