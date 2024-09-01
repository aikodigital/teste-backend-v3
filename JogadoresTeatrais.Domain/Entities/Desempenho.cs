
namespace JogadoresTeatrais.Domain.Entities
{
    public class Desempenho
    {
        public int Id { get; set; }

        public int JogarId { get; set; }

        public int Audiencia { get; set; }

        public int FaturaId { get; set; }
        
        public Fatura? Fatura { get; set; }
    }
}
