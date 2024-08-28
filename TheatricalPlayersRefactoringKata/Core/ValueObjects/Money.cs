using System;

namespace TheatricalPlayersRefactoringKata.Core.ValueObjects;

public class Money
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    public Money(decimal amount, string currency = "USD")
    {
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (other.Currency != Currency)
            throw new InvalidOperationException("Cannot add amounts with different currencies.");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.Currency != Currency)
            throw new InvalidOperationException("Cannot subtract amounts with different currencies.");

        return new Money(Amount - other.Amount, Currency);
    }

    public override string ToString()
    {
        return $"{Currency} {Amount:N2}";
    }
}

