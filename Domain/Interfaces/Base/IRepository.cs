using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Base
{
    public interface IRepository<TEntity>
    {
        public TEntity Add(TEntity entity);
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        public  IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        public  TEntity Get(int id);
        public TEntity Remove(TEntity entity);
        public  IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        public  TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        public Task<TEntity> Update(TEntity entity);
     
        public Task<TEntity> AddAsync(TEntity entity);
        public  Task<List<TEntity>> GetAllLisAsync();

    }
}
