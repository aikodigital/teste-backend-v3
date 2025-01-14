namespace TheatricalPlayersRefactoringKata.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
