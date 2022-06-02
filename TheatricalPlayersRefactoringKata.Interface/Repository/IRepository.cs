using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        TEntity Get(long id);
        List<TEntity> Get(List<long> ids);
    }
}