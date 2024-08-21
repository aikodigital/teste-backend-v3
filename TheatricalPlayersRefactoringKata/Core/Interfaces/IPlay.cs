namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPlay
{
    public string Name { get; set; }

    public int Lines { get; set; } 

    public string Type { get; set; }
    
    public int CalculateAmount(int audience);
}