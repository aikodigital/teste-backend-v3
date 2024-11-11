namespace TheatricalPlayersRefactoringKata.Repository.Interfaces;

public interface IRepository<T>
{
    bool Add(T entity);
    List<T> GetAll();
    T? GetById(int id);
    bool Update(int id, T entity);
    bool Remove(int id);
}