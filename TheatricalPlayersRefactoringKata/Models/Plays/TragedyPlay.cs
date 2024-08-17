
namespace TheatricalPlayersRefactoringKata.Models;
public class TragedyPlay(string name, int lines) : Play(name, lines)
{
    public override string Type { get => "tragedy"; }

    public static int StaticCalculateCharge(int audience, int lines) {
        int baseCost = Play.StaticCalculateCharge(lines);
        if (audience > 30) {
            baseCost += 1000 * (audience - 30);
        }
        return baseCost;
    }

    public override int CalculateCharge(int audience)
    {
        return StaticCalculateCharge(audience, lines);
    }
    
}