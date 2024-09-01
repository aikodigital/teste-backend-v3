using JogadoresTeatrais.Domain.Entities;
using JogadoresTeatrais.Domain.Interfaces;
using JogaresTeatrais.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace JogaresTeatrais.Data.Repositories
{
    public class FaturaRepository : Repository<Fatura>, IFaturaRepository
    {
        public FaturaRepository(DataContext context) : base(context) { }

        public IEnumerable<Fatura> GetAll()
        {
            return _context.Fatura.Include(f => f.Desempenhos).ToList();
        }
    }
}