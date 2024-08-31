using JogadoresTeatrais.Domain.Entities;
using JogadoresTeatrais.Domain.Interfaces;
using JogaresTeatrais.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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