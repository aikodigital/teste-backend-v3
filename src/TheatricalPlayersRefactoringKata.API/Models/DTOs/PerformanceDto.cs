using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.API.Models.DTOs;

public class PerformanceDto
{
    [Required]
    public PlayDto Play { get; set; } = null!;

    [Required]
    public int Audience { get; set; }
}