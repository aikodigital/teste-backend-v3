using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TheatricalPlayersAPI.Models;

public class PlayModel
{
    [Key][JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Genre { get; set; }
}