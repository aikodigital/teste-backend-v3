using System;

namespace TheatricalPlayersRefactoringKata.Models;
public class HistoryPlay(string name, int lines) : Play(name, lines)
{
    public override string Type { get => "history"; }


    public override int CalculateCharge(int audience)
    {
        int baseCost = ComedyPlay.StaticCalculateCharge(audience, _lines);
        baseCost += TragedyPlay.StaticCalculateCharge(audience, _lines);
        return baseCost;
    }
    
}