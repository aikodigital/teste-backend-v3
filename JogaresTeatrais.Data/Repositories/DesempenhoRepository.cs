using JogadoresTeatrais.Domain.Entities;
using JogadoresTeatrais.Domain.Interfaces;
using JogaresTeatrais.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JogaresTeatrais.Data.Repositories
{
    public class DesempenhoRepository : Repository<Desempenho>, IDesempenhoRepository
    {
        public DesempenhoRepository(DataContext context) : base(context) { }

        public IEnumerable<Desempenho> GetAll()
        {
            return Query(x => x.Id > 0);
        }
    }
}
