using JogadoresTeatrais.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using JogaresTeatrais.Data.Context;

namespace JogaresTeatrais.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region 'Properties'

        protected readonly DataContext _context;

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        #endregion

        public Repository(DataContext context)
        {
            _context = context;
        }

        #region 'Methods: Create/Update/Remove/Save'

        public TEntity Create(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                Save();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TEntity> Create(List<TEntity> models)
        {
            try
            {
                DbSet.AddRange(models);
                Save();
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                EntityEntry<TEntity> entry = NewMethod(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                return Save() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private EntityEntry<TEntity> NewMethod(TEntity model)
        {
            return _context.Entry(model);
        }

        public bool Update(List<TEntity> models)
        {
            try
            {
                foreach (TEntity register in models)
                {
                    EntityEntry<TEntity> entry = _context.Entry(register);
                    DbSet.Attach(register);
                    entry.State = EntityState.Modified;
                }

                return Save() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(TEntity model)
        {
            try
            {
               
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                EntityEntry<TEntity> _entry = _context.Entry(model);

                if (_entry.State == EntityState.Detached)
                {
                    DbSet.Attach(model); 
                }
               
                _entry.State = EntityState.Deleted;
                
                return Save() > 0;
            }
            catch (Exception)
            {
                throw; 
            }
        }

        public bool Delete(params object[] Keys)
        {
            try
            {
                TEntity model = DbSet.Find(Keys);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                TEntity model = DbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();

                return (model != null) && Delete(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 'Methods: Search'

        public TEntity Find(params object[] Keys)
        {
            try
            {
                return DbSet.Find(Keys);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.AsNoTracking().FirstOrDefault(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = DbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.SingleOrDefault(predicate);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.Where(where);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            try
            {
                IQueryable<TEntity> _query = DbSet;

                if (includes != null)
                    _query = includes(_query) as IQueryable<TEntity>;

                return _query.Where(predicate).AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 'Assyncronous Methods'

        public async Task<TEntity> CreateAsync(TEntity model)
        {
            try
            {
                DbSet.Add(model);
                await SaveAsync();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            try
            {
                EntityEntry<TEntity> entry = _context.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            try
            {
                EntityEntry<TEntity> entry = _context.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Deleted;

                return await SaveAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(params object[] Keys)
        {
            try
            {
                TEntity model = DbSet.Find(Keys);
                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                TEntity model = DbSet.FirstOrDefault(where);

                return (model != null) && await DeleteAsync(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region 'Search Methods Async'

        public async Task<TEntity> GetAsync(params object[] Keys)
        {
            try
            {
                return await DbSet.FindAsync(Keys);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        public void Dispose()
        {
            try
            {
                if (_context != null)
                    _context.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
