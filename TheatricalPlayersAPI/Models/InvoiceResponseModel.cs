namespace TheatricalPlayersAPI.Models;

public class InvoiceResponseModel
{
    public string Customer { get; set; }
    public List<PerformanceResponseModel>? Performances { get; set; }
}