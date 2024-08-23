#region

using TheatricalPlayersRefactoringKata.Core.Services;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPlay
{
    public string Name { get; }

    public Genre Type { get; }
}