using System;
using TheatricalPlayersRefactoringKata.Exceptions;

namespace TheatricalPlayersRefactoringKata.Core.ValueObjects;

public class Credits
{
    public int Value { get; }

    public Credits(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException(ResourceMessagesException.CREDIT_NEGATIVE_ERROR);
        }
        Value = value;
    }

    public Credits Add(Credits other)
    {
        return new Credits(Value + other.Value);
    }

    public Credits Subtract(Credits other)
    {
        if (Value < other.Value)
        {
            throw new InvalidOperationException(ResourceMessagesException.SUBTRACT_CREDIT_ERROR);
        }
        return new Credits(Value - other.Value);
    }
}

