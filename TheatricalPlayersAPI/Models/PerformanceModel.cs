using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TheatricalPlayersAPI.Models;

public class PerformanceModel
{
    [Key][JsonIgnore]
    public int Id { get; set; }
    public string PlayId { get; set; }
    public int Audience { get; set; }
    [JsonIgnore]
    public string? PlayGenre { get; set; }
    [JsonIgnore]
    public int InvoiceModelId { get; set; }
}