using System;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersRefactoringKata.Core.ValueObjects;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException(ResourceMessagesException.UKNNOWN_ERROR);
        }

        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException(ResourceMessagesException.AMOUNTS_DIFFEREN_ERROR);
        }
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException(ResourceMessagesException.SUBTRACT_AMOUNTS_DIFFERENTE_ERROR);
        }
        if (Amount < other.Amount)
        {
            throw new InvalidOperationException(ResourceMessagesException.UKNNOWN_ERROR);
        }
        return new Money(Amount - other.Amount, Currency);
    }
}
