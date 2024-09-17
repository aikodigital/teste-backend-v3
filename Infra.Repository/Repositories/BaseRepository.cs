using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Context;
using System.Data.Entity;

namespace Infra.Repository.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public void Create(T entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    public void Update(T entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
    }

    public void Delete(T entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    public async Task<T> GetById(int id)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<T>> GetAll()
    {
        return await Context.Set<T>().ToListAsync();
    }
}