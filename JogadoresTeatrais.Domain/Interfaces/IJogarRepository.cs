using JogaresTeatrais.Data;

namespace JogadoresTeatrais.Domain.Interfaces
{
    public interface IJogarRepository : IRepository<Jogar>
    { 
        IEnumerable<Jogar> GetAll();
    }
}
