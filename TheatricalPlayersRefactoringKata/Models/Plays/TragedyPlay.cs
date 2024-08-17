
namespace TheatricalPlayersRefactoringKata.Models;
public class TragedyPlay(string name, int lines) : Play(name,lines)
{
    public override string Type { get => "tragedy"; }

    public override int CalculateCharge(int audience)
    {
        int baseCost = base.CalculateCharge(audience);
        if (audience > 30) {
            baseCost += 1000 * (audience - 30);
        }
        return baseCost;
    }
    
}