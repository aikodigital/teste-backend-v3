namespace TheatricalPlayersAPI.Models;

public class InvoiceModel
{
    public int Id {get; set;}
    public string Customer { get; set; }
    public List<PerformanceModel> Performances { get; set; }
}