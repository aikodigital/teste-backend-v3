using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersAPI.Models;

public class PerformanceModel
{
    [Key]
    public int Id { get; set; }
    public string PlayId { get; set; }
    public int Audience { get; set; }
    public string PlayGenre { get; set; }
}