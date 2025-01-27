using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Repository
{
    public interface ICrudGenerico<T>
    {
        Task Add(T entidade);
        Task<long> AddReturnById(T entidade);
        Task Update(T entidade);
        Task Delete(T entidade);
        Task<List<T>> GetAll();
        Task<List<T>> GetByFilter(Expression<Func<T, bool>> filtro = null, params Expression<Func<T, object>>[] includes);
        Task<T?> GetById(long id);
    }
}
