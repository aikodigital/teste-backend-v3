namespace TheatherPlayersInfra.DataAccess;

internal class UnitOfWork : IUnityOfWork
{
    private readonly TheatherPlayersDbContext _dbContext;
    public UnitOfWork(TheatherPlayersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
