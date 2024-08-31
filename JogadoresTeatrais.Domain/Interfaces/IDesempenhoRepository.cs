using JogadoresTeatrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogadoresTeatrais.Domain.Interfaces
{
    public interface IDesempenhoRepository : IRepository<Desempenho>
    {
       IEnumerable<Desempenho> GetAll();
    }
}
