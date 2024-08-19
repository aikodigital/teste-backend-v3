using Microsoft.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories.Common
{
    public class Repository<T> where T : class
    {
        protected readonly DbContextTheatricalPlayers _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContextTheatricalPlayers context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}