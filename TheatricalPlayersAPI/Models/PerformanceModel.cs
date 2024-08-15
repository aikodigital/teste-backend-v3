using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersAPI.Models;

public class PerformanceModel
{
    public string PlayId { get; set; }
    public int Audience { get; set; }
    public string PlayGenre { get; set; }
}