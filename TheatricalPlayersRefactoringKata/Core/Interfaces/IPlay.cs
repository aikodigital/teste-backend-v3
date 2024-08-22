using TheatricalPlayersRefactoringKata.Core.Services;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPlay
{
    public string Name { get; }

    public Genre Type { get; }
}