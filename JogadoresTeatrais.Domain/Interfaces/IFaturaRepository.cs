using JogadoresTeatrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogadoresTeatrais.Domain.Interfaces
{
    public interface IFaturaRepository : IRepository<Fatura>
    {
        IEnumerable<Fatura> GetAll();
    }
}
