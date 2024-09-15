namespace TheatricalPlayersRefactoringKata;

public class HistoricalPlay : Play
{

    public HistoricalPlay(string name, int lines) : base(name, lines) { }
    public override decimal CalculateValue(int audience)
    {
        decimal tragedyPart = new TragedyPlay(Name, Lines).CalculateValue(audience);
        decimal comedyPart = new ComedyPlay(Name, Lines).CalculateValue(audience);

        return tragedyPart + comedyPart;
    }

}