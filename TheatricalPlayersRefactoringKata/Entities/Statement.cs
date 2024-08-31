using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Statement
{
    public int Id { get; set; }
    public string Customer { get; set; }
    public decimal TotalAmountOwed { get; set; }
    public decimal TotalEarnedCredits { get; set; }
    public List<StatementItem> Items { get; set; }
}