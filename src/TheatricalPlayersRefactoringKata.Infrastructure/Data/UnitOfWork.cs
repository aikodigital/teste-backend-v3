namespace TheatricalPlayersRefactoringKata.Infrastructure.Data;

/// <summary>
/// Represents a unit of work that encapsulates a DbContext and provides methods to commit changes and dispose of the context.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified DbContext.
    /// </summary>
    /// <param name="context">The DbContext to be used by this unit of work.</param>
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Commits all changes made in this context to the database asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous save operation. The task result contains true if the changes were successfully committed; otherwise, false.</returns>
    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// Disposes the DbContext.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }
}