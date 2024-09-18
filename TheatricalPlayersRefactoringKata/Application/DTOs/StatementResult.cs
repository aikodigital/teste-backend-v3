using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Application.DTOs;

public class StatementResult
{
    public string Customer { get; set; }
    public List<StatementLine> Lines { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalVolumeCredits { get; set; }
}

public class StatementLine
{
    public string PlayName { get; set; }
    public int Audience { get; set; }
    public decimal Amount { get; set; }
    public int VolumeCredits { get; set; }
}
