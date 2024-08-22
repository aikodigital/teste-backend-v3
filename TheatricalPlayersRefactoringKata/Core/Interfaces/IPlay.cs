namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPlay
{
    public string Name { get; }

    public string Type { get; }
    
    public int CalculateAmount(int audience);
}