namespace TheatricalPlayersRefactoringKata.Domain.Models;

/// <summary>
/// Represents a performance in the theatrical domain.
/// </summary>
public class Performance : Entity
{
    // Parameterless constructor EF Core
    protected Performance() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Performance"/> class.
    /// </summary>
    /// <param name="play">The play being performed.</param>
    /// <param name="audience">The size of the audience.</param>
    public Performance(Play play, int audience)
    {
        Play = play;
        Audience = audience;
    }

    /// <summary>
    /// Gets the play being performed.
    /// </summary>
    public Play Play { get; private set; } = null!;

    /// <summary>
    /// Gets the size of the audience.
    /// </summary>
    public int Audience { get; private set; }

    /// <summary>
    /// Gets or sets the amount for the performance.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the credits for the performance.
    /// </summary>
    public int Credits { get; set; }
}