namespace TheatricalPlayersRefactoringKata.Models
{
    public class Performance
    {
        public int Id { get; set; } // Adicione esta linha se necessário
        public int PlayId { get; set; }
        public Play Play { get; set; }
        public int Seats { get; set; }

        // Construtor com parâmetros
        public Performance(int playId, int seats)
        {
            PlayId = playId;
            Seats = seats;
        }

        // Construtor padrão
        public Performance() { }
    }
}
