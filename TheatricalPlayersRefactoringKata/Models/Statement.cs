using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Models;

public class Statement
{
    public string Customer { get; set; }
    public List<StatementItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public int TotalCredits { get; set; }
}

public class StatementItem
{
    public string PlayName { get; set; }
    public decimal AmountOwed { get; set; }
    public int EarnedCredits { get; set; }
    public int Seats { get; set; }
}
