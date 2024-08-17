using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml;

namespace TheatricalPlayersAPI.Models;

public class StatementModel
{
    [Key][JsonIgnore]
    public int Id { get; set; }
    public string? Statement { get; set; }
    public string? StatementXml { get; set; }
}