namespace TheatricalPlayersRefactoringKata.Entities;

public class StatementItemEntity
{
    public string Name { get; private set; }
    
    public decimal AmountOwed { get; private set; }

    public int EarnedCredits { get; private set; }

    public int Seats { get; private set; }

    public StatementItemEntity(string name, decimal amountOwed, int earnedCredits, int seats)
    {
        Name = name;
        AmountOwed = amountOwed;
        EarnedCredits = earnedCredits;
        Seats = seats;
    }
}