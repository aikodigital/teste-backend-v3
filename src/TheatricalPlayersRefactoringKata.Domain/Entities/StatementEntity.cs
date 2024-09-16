using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TheatricalPlayersRefactoringKata.Entities;

public class StatementEntity
{
    public string Customer { get; private set; }

    private readonly List<StatementItemEntity> _items = new();
    public ReadOnlyCollection<StatementItemEntity> Items => _items.AsReadOnly();

    public decimal AmountOwed { get; private set; }

    public int EarnedCredits { get; private set; }

    public StatementEntity(string customer)
    {
        Customer = customer;
    }

    public void AddItem(string name, decimal amountOwed, int earnedCredits, int seats)
    {
        var item = new StatementItemEntity(
            name: name,
            amountOwed: amountOwed,
            earnedCredits: earnedCredits,
            seats: seats
        );
        _items.Add(item);
    }
    
    public void Close(decimal amountOwed, int earnedCredits)
    {
        AmountOwed = amountOwed;
        EarnedCredits = earnedCredits;
    }
}