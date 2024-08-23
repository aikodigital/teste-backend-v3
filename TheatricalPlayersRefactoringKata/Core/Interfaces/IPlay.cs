#region

using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Core.Services;

#endregion

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPlay
{
    public Guid Id { get; init; }
    public int Lines { get; init; }

    [MaxLength (30)]
    public string Name { get; init; }
    public Genre Type { get; init; }
}