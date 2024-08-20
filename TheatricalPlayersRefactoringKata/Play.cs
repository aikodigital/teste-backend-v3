using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;


public abstract class Play
{
    public string Name { get; private set; }
    public int Lines { get; private set; }

    public Play(string name, int lines)
    {
        Name = name;
        Lines = PlayLines(lines);
    }

    private int PlayLines(int lines)
    {
        if (lines < 1000) return 1000;
        if (lines > 4000) return 4000;
        return lines;
    }

    public abstract decimal CalculateAmount(int audience);
    public abstract int CalculateCredits(int audience);

}

public class TragedyPlay : Play
{
    public TragedyPlay(string name, int lines) : base(name, lines) { }
    public override decimal CalculateAmount(int audience)
    {
        decimal amount = Lines * 10;
        if (audience >= 30)
        {
            amount += 1000 * (audience - 30);
        }
        return amount;

    }

    public override int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}

public class ComedyPlay : Play
{
    public ComedyPlay(string name, int lines) : base(name, lines) { }
    public override decimal CalculateAmount(int audience)
    {
        decimal amount = Lines * 10;
        amount += 300 * audience;
        if (audience > 20)
        {
            amount += 10000 + 500 * (audience - 20);
        }
        return amount;

    }

    public override int CalculateCredits(int audience)
    {
        int credits = Math.Max(audience - 30, 0);
        credits += (int)Math.Floor((decimal)audience / 5);
        return credits;
    }
}

public class HistoryPlay : Play
{
    public HistoryPlay(string name, int lines) : base(name, lines) { }

    public override decimal CalculateAmount(int audience)
    {
        decimal tragedyAmount = new TragedyPlay(Name, Lines).CalculateAmount(audience);
        decimal comedyAmount = new ComedyPlay(Name, Lines).CalculateAmount(audience);
        return tragedyAmount + comedyAmount;
    }

    public override int CalculateCredits(int audience)
    {
        return new TragedyPlay(Name, Lines).CalculateCredits(audience);
    }
}

