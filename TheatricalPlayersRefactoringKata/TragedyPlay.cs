using System;

namespace TheatricalPlayersRefactoringKata;

public class TragedyPlay : Play
{

    public TragedyPlay(string name, int lines) : base(name, lines) { }
    public override decimal CalculateValue(int audience)
    {
        var amount = 0;
        var playLines = this.Lines;

        if (playLines < 1000) playLines = 1000;
        if (playLines > 4000) playLines = 4000;

        amount += playLines * 10;

        if (audience > 30)
        {
            amount += 1000 * (audience - 30);
        }
        return amount;
    }

}