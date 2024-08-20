using System;

namespace TheatricalPlayersRefactoringKata;

public class Play
{
    public string Name { get; set; }
    public int NumberLines { get; set; }
    public int Audience { get; set; }

    protected const int MinLines = 1000;
    protected const int MaxLines = 4000;

    protected virtual double CalculatorValorBase()
    {
        throw new NotImplementedException("This method must be implemented in a derived class.");
    }

    public virtual double CalculatorValor()
    {
        return CalculatorValorBase();
    }

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
