using JogadoresTeatrais.Domain.Entities;

namespace JogadoresTeatrais.Domain.Interfaces
{
    public interface IFaturaRepository : IRepository<Fatura>
    {
        IEnumerable<Fatura> GetAll();
    }
}
