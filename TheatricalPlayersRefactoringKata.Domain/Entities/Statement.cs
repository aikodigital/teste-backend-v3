using System.Collections.Generic;
namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Statement
{ 
    public string Customer { get; set; }
    public List<Item> Items { get; set; } 
    public decimal AmountOwed { get; set; }
    public int EarnedCredits { get; set; }
}

public class Item
{
    public string Name { get; set;} 
    public decimal AmountOwed { get; set; }
    public int EarnedCredits { get; set; }
    public int Seats { get; set; }
}