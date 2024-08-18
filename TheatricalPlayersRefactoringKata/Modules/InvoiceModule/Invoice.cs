namespace TheatricalPlayersRefactoringKata.Modules;

public class Invoice
{
    private string _customer;
    private List<Performance> _performances;
    private InvoiceResults? _results;

    public string Customer { get => _customer; set => _customer = value; }
    public List<Performance> Performances { get => _performances; set => _performances = value; }
    public InvoiceResults? Results { get => _results; set => _results = value; }

    public Invoice(string customer, List<Performance> performance)
    {
        this._customer = customer;
        this._performances = performance;
    }

    public Invoice(string customer, List<Performance> performance, InvoiceResults results)
    {
        this._customer = customer;
        this._performances = performance;
        this._results = results;
    }
}