using System;

namespace TheatricalPlayersRefactoringKata;

public class Play
{
    public string Name { get; set; }
    public int NumberLines { get; set; }
    public int Audience { get; set; }

    protected const int MinLines = 1000;
    protected const int MaxLines = 4000;

    /// <summary>
    /// Calculates the base value based on the number of lines in the part.
    /// The number of lines is adjusted to be within the range defined by the minimum and maximum values
    /// specified in the part. The returned value is the number of rows divided by 10.
    /// </summary>
    /// <returns>
    /// The calculated base value, which is the adjusted number of rows divided by 10.0.
    /// </returns>
    protected virtual double CalculatorValorBase()
    {
        int lines = Math.Clamp(NumberLines, MinLines, MaxLines);
        return lines / 10.0;
    }

    /// <summary>
    /// Calculates the total value using the calculation base defined by the <see cref="CalculatorValorBase"/> method.
    /// This method can be overridden in subclasses to provide custom calculation logic.
    /// </summary>
    /// <returns>
    /// The total value calculated based on the implementation of the <see cref="CalculatorValorBase"/> method.
    /// </returns>
    public virtual double CalculatorValor()
    {
        return CalculatorValorBase();
    }

    /// <summary>
    /// Calculates credits based on the number of people in the audience.
    /// If the audience is less than or equal to 30, the method returns 0 credits.
    /// For audiences greater than 30, the method calculates additional credits, 
    /// returning the maximum value between audience minus 30 and 0.
    /// </summary>
    /// <returns>
    /// The calculated number of credits, which is 0 if the audience is less than or equal to 30,
    /// or the audience value minus 30 if greater, ensuring that the value is not negative.
    /// </returns>
    public virtual int CalculatorCredits()
    {
        if (Audience <= 30)
        {
            return 0;
        }
        return Math.Max(Audience - 30, 0);
    }

    public Play(string name, int lines)
    {
        this.Name = name;
        this.NumberLines = lines;
    }
}
