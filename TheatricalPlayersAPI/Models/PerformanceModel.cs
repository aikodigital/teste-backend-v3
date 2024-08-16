using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersAPI.Models;

public class PerformanceModel
{
    [Key]
    private int Id { get; set; }
    public string PlayId { get; set; }
    public int Audience { get; set; }
    public string PlayGenre { get; set; }
}