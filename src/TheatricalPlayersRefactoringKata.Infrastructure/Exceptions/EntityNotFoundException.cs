namespace TheatricalPlayersRefactoringKata.Infrastructure.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found in the database.
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public EntityNotFoundException(string message) : base(message) { }
}