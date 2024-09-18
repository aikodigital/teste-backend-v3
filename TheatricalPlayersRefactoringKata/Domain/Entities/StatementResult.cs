using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class StatementResult
{
    [Key]
    public int Id { get; set; }
    public string Customer { get; set; }
    public List<StatementLine> Lines { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalVolumeCredits { get; set; }
}

public class StatementLine
{
    [Key]
    public int Id { get; set; }
    public string PlayName { get; set; }
    public int Audience { get; set; }
    public decimal Amount { get; set; }
    public int VolumeCredits { get; set; }
}
