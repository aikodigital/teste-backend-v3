using TheatricalPlayersRefactoringKata.Factories;

namespace TheatricalPlayersRefactoringKata.Models;

public class Play
{
    private string _name;
    private int _lines;
    private string _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Type { get => _type; set => _type = value; }

    public Play(string name, int lines, string type)
    {
        _name = name;
        _lines = lines;
        _type = type;
    }

    public double Perform(double thisAmount, int audience)
    {
        var strategy = PlayStrategyFactory.GetStrategy(_type);
        return strategy.Execute(thisAmount, audience);
    }
}
