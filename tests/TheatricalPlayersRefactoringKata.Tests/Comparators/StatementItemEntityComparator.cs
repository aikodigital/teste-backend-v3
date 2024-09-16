using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Tests.Comparators;

public class StatementItemEntityComparator : IEqualityComparer<StatementItemEntity>
{
    public bool Equals(StatementItemEntity x, StatementItemEntity y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Name == y.Name && x.AmountOwed == y.AmountOwed && x.EarnedCredits == y.EarnedCredits && x.Seats == y.Seats;
    }

    public int GetHashCode(StatementItemEntity obj)
    {
        return HashCode.Combine(obj.Name, obj.AmountOwed, obj.EarnedCredits, obj.Seats);
    }
}