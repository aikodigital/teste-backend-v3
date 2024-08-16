using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheatricalPlayersAPI.Models;

public class InvoiceModel
{
    [Key][JsonIgnore]
    public int Id {get; set;}
    public string Customer { get; set; }
    public List<PerformanceModel> Performances { get; set; }
}