using System;

namespace TheatricalPlayersRefactoringKata.Core.ValueObjects;

public class Credits
{
    public int Value { get; private set; }

    public Credits(int value)
    {
        if (value < 0)
            throw new ArgumentException("Credits cannot be negative.");

        Value = value;
    }

    public Credits Add(Credits other)
    {
        return new Credits(Value + other.Value);
    }

    public Credits Subtract(Credits other)
    {
        if (other.Value > Value)
            throw new InvalidOperationException("Cannot subtract more credits than available.");

        return new Credits(Value - other.Value);
    }

    public override string ToString()
    {
        return $"{Value} credits";
    }
}

