using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TheatricalPlayersAPI.Models;

public class PerformanceModel
{
    [Key][IgnoreDataMember]
    public int Id { get; set; }
    public string PlayId { get; set; }
    public int Audience { get; set; }
    public string PlayGenre { get; set; }
}