using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogadoresTeatrais.Domain.Entities
{
    public class Fatura
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public List<Desempenho> Desempenhos { get; set; }
    }
}
