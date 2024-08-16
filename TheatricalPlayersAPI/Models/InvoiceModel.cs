using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersAPI.Models;

public class InvoiceModel
{
    [Key]
    private int Id {get; set;}
    public string Customer { get; set; }
    public List<PerformanceModel> Performances { get; set; }
}