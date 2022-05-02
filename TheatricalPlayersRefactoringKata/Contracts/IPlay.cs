namespace TheatricalPlayersRefactoringKata.Contracts;

public interface IPlay
{
    int Lines { get; set; }
    string Name { get; set; }
    string Type { get; set; }
}