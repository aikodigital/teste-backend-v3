using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.DTO;
public class InvoiceDto
{
    public string Customer { get; set; }
    public List<PerformanceDto> Performances { get; set; }
    public List<PlayDto> Plays { get; set; }
}
