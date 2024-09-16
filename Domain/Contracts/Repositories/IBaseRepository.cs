using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);  
        void Delete(T entity);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
    }
}
