using TheatricalPlayersRefactoringKata.Types;
using TheatricalPlayersRefactoringKata;

public class Historic : Play
{
    private Tragedy tragedies;
    private Comedy comedies;

    public Historic(string name, int lines) : base(name, lines)
    {
        tragedies = new Tragedy(name, lines);
        comedies = new Comedy(name, lines);
    }

    public void SetAudience(int audience)
    {
        Audience = audience;
        tragedies.Audience = audience;
        comedies.Audience = audience;
    }

    protected override double CalculatorValorBase()
    {
        return tragedies.CalculatorValor() + comedies.CalculatorValor();
    }

    public override double CalculatorValor()
    {
        return CalculatorValorBase();
    }
}