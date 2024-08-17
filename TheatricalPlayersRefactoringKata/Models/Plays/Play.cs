using System;

namespace TheatricalPlayersRefactoringKata.Models;
public abstract class Play(string name, int lines)
{
    protected string _name = name;
    protected int _lines = lines;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public abstract string Type { get; }

    public virtual int CalculateCharge(int audience) {
        return StaticCalculateCharge(_lines);
    }

    protected static int StaticCalculateCharge(int lines) {
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        return lines / 10; // TODO: Determine if result should be truncated as now or rounded.
    }

    public virtual int CalculateCredits(int audience) {
        return Math.Max(audience - 30, 0);
    }
    
    
}
