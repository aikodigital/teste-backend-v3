using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.API.Models.DTOs;

public class InvoiceDto
{
    [Required]
    public CustomerDto Customer { get; set; } = null!;
    [Required]
    public List<PerformanceDto> Performances { get; set; } = null!;
}