using JogadoresTeatrais.Domain.Entities;


namespace JogadoresTeatrais.Domain.Interfaces
{
    public interface IDesempenhoRepository : IRepository<Desempenho>
    {
       IEnumerable<Desempenho> GetAll();
    }
}
