using System;

namespace TheatricalPlayersRefactoringKata.Models;
public class ComedyPlay(string name, int lines) : Play(name, lines)
{
    public override string Type { get => "comedy"; }

    public static int StaticCalculateCharge(int audience, int lines) {
        int baseCost = Play.StaticCalculateCharge(lines);
        if (audience > 20) {
            baseCost += 10000 + 500 * (audience - 20);
        }
        baseCost += 300 * audience;
        return baseCost;
    }

    public override int CalculateCharge(int audience)
    {
        return StaticCalculateCharge(audience, _lines);
    }

    public override int CalculateCredits(int audience)
    {
        int baseCredits = base.CalculateCredits(audience);
        baseCredits += (int) Math.Floor((decimal) audience / 5);
        
        return baseCredits;
    }
}