using JogadoresTeatrais.Domain.Entities;

namespace JogadoresTeatrais.Application.ViewModels
{
    public class FaturaViewModel    
    {
        public int Id {get; set;}
        public string? Cliente { get; set;}
        public List<Desempenho>? Desempenhos { get; set; }

    }
}
