using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IPerformance
{
    public Guid Id { get; }
    public Guid PlayId { get; init; }
    public Play Play { get; set; }

    public int Audience { get; init; }

    public int Amount { get; init; }
}