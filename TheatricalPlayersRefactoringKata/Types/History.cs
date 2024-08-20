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

    /// <summary>
    /// Defines the number of people in the audience.
    /// This value is assigned to the <see cref="Audience"/> property and 
    /// is also updated in instances of <see cref="tragedies"/> and <see cref="comedies"/>.
    /// </summary>
    /// <param name="audience">
    /// The number of people in the audience. This value will be applied to the general audience,
    /// as well as instances of <see cref="tragedies"/> and <see cref="comedies"/>.
    /// </param>
    public void SetAudience(int audience)
    {
        Audience = audience;
        tragedies.Audience = audience;
        comedies.Audience = audience;
    }

    /// <summary>
    /// Calculates the base value based on the number of lines in the part.
    /// The number of lines is clamped to the range defined by the minimum and maximum value
    /// specified in the part. The returned value is the number of rows divided by 10.
    /// </summary>
    /// <returns>
    /// The calculated base value, which is the number of rows divided by 10.
    /// </returns>
    protected override double CalculatorValorBase()
    {
        return tragedies.CalculatorValor() + comedies.CalculatorValor();
    }

    /// <summary>
    /// Calculates the total value based on the value returned by the base method.
    /// This method can be overridden in subclasses to provide custom calculation logic.
    /// </summary>
    /// <returns>
    /// The total value calculated by the base method <see cref="CalculatorValorBase"/>.
    /// </returns>
    public override double CalculatorValor()
    {
        return CalculatorValorBase();
    }
}