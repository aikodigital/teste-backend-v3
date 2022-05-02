using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata;

public class Play : IPlay
{
    public string Name { get; set; }
    public int Lines { get; set; }

    public Play(string name, int lines)
    {
        Name = name;
        Lines = lines;
    }
}
