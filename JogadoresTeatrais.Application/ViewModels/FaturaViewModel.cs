using JogadoresTeatrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogadoresTeatrais.Application.ViewModels
{
    public class FaturaViewModel    
    {
        public int Id {get; set;}
        public string Cliente { get; set;}
        public List<Desempenho> Desempenhos { get; set; }

    }
}
