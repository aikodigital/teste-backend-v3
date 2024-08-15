using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersAPI.Models;

public class PlayModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Lines { get; set; }
    public int Genre { get; set; }
}