namespace TheatricalPlayersRefactoringKata;

public readonly struct StatementEntry
{
    public readonly decimal Amount;
    public readonly int Credits;

    public StatementEntry(decimal amount, int credits)
    {
        Amount = amount;
        Credits = credits;
    }
}