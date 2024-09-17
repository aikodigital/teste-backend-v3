using System;

namespace TheatricalPlayersRefactoringKata.Entities;

public class ComedyPlay : Play
{

    public ComedyPlay(string name, int lines) : base(name, lines) { }
    public override decimal CalculateValue(int audience)
    {

        var amount = 0;
        var playLines = Lines;

        if (playLines < 1000) playLines = 1000;
        if (playLines > 4000) playLines = 4000;

        amount += playLines * 10;
        amount += 300 * audience;

        if (audience > 20)
        {
            amount += 10000 + 500 * (audience - 20);
        }
        return amount;
    }

    public override int CalculateCredits(int audience)
    {
        return Math.Max(audience - 30, 0) + (int)Math.Floor((decimal)audience / 5);

    }
}