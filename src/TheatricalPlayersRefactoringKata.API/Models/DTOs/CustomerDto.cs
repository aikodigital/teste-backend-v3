using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.API.Models.DTOs;

public class CustomerDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = null!;
};
