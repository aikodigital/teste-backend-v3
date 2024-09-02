using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.API.Models.DTOs;

public class PlayDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Genre { get; set; } = null!;

    [Required]
    [Range(1000, 4000, ErrorMessage = "Audience must be between 1000 and 4000.")]
    public int Lines { get; set; }
}