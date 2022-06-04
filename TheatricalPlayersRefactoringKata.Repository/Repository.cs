using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Interface.Repository;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;
using TheatricalPlayersRefactoringKata.Repository.Configuration.Context;

namespace TheatricalPlayersRefactoringKata.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ITheatricalContext _theatricalContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ITheatricalContext theatricalContext)
        {
            _theatricalContext = theatricalContext;
            DbSet = _theatricalContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            TEntity entityCreated = DbSet.Add(entity).Entity;

            _theatricalContext.SaveChanges();

            return entityCreated;
        }

        public void AddRange(List<TEntity> entities)
        {
            DbSet.AddRange(entities);
            _theatricalContext.SaveChanges();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntity entityCreated = (await DbSet.AddAsync(entity)).Entity;

            _theatricalContext.SaveChanges();

            return entityCreated;
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            _theatricalContext.SaveChanges();
        }
        
        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
            _theatricalContext.SaveChanges();
        }

        public void UpdateRange(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            _theatricalContext.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            _theatricalContext.SaveChanges();
        }

        public void RemoveRange(List<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            _theatricalContext.SaveChanges();
        }

        public virtual TEntity Get(long id)
        {
            TEntity entity = DbSet.FirstOrDefault(data => data.Id == id);

            return entity;
        }

        public virtual List<TEntity> Get(List<long> ids)
        {
            List<TEntity> entities = DbSet.Where(data => ids.Contains(data.Id))?.ToList();

            return entities;
        }

        public virtual async Task<TEntity> GetAsync(long id)
        {
            TEntity entity = await DbSet.FirstOrDefaultAsync(data => data.Id == id);

            return entity;
        }

        public virtual async Task<List<TEntity>> GetAsync(List<long> ids)
        {
            List<TEntity> entities = await DbSet.Where(data => ids.Contains(data.Id))?.ToListAsync();

            return entities;
        }
    }
}