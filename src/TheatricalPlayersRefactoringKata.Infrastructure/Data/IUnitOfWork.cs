namespace TheatricalPlayersRefactoringKata.Infrastructure.Data;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
}

