using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TheatricalPlayersAPI.Models;

public class PlayModel
{
    [Key][JsonIgnore]
    private int Id { get; set; }
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Genre { get; set; }
}