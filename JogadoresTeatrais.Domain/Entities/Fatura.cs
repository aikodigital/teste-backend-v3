
namespace JogadoresTeatrais.Domain.Entities
{
    public class Fatura
    {
        public int Id { get; set; }
        public string? Cliente { get; set; }
        public List<Desempenho>? Desempenhos { get; set; }
    }
}
