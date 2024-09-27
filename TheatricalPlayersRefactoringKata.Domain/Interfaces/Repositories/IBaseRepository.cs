using System.Linq.Expressions;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;

public interface IBaseRepository<TObject> where TObject : class
{
    public Task AddAsync(TObject entity);
    public Task<List<TObject>> GetAll();
    public Task<TObject> GetById(int id);
    public Task<List<TObject>> GetByFilter(Expression<Func<TObject, bool>> predicate);
    public Task RemoveAsync(int id);
    public Task UpdateAsync(TObject entity);
}
