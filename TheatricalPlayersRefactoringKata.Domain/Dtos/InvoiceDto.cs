using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Dtos;

public class InvoiceDto
{
    public string Customer { get; set; }

    public ICollection<PerformanceDto> Performances { get; set; } = new List<PerformanceDto>();
}
