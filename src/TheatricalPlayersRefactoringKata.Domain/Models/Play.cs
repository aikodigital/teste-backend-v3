using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Models;

/// <summary>
/// Represents a play in the theatrical domain.
/// </summary>
public class Play : Entity
{
    // Parameterless constructor EF Core
    protected Play() {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Play"/> class.
    /// </summary>
    /// <param name="title">The title of the play.</param>
    /// <param name="genre">The genre of the play.</param>
    /// <param name="lines">The number of lines in the play.</param>
    public Play(string title, GenreEnum genre, int lines)
    {
        Title = title;
        Genre = genre;
        Lines = lines;
    }

    /// <summary>
    /// Gets the title of the play.
    /// </summary>
    public string Title { get; private set; } = null!;

    /// <summary>
    /// Gets the genre of the play.
    /// </summary>
    public GenreEnum Genre { get; private set; }

    /// <summary>
    /// Gets the number of lines in the play.
    /// </summary>
    public int Lines { get; private set; }

    public void ValidateLines()
    {
        if (Lines is < 1000 or > 4000)
        {
            throw new ArgumentOutOfRangeException(nameof(Lines), "Lines must be between 1000 and 4000.");
        }
    }
}
