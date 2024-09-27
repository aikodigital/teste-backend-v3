using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class BaseRepository<TObject, TContext> : IBaseRepository<TObject> where TObject : class where TContext : DbContext
{
    protected ApplicationDbContext _dbContext { get; set; }

    public BaseRepository(ApplicationDbContext context) => _dbContext = context;
    
    public async Task AddAsync(TObject entity)
    {
        await _dbContext.Set<TObject>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<TObject>> GetAll() => await _dbContext.Set<TObject>().ToListAsync();

    public async Task<TObject> GetById(int id) => await _dbContext.Set<TObject>().FindAsync(id);

    public async Task<List<TObject>> GetByFilter(Expression<Func<TObject, bool>> predicate) => await _dbContext.Set<TObject>().Where(predicate).ToListAsync();

    public async Task RemoveAsync(int id)
    {
        _dbContext.Set<TObject>().Remove(await GetById(id));

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TObject entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;

        await _dbContext.SaveChangesAsync();
    }
}
