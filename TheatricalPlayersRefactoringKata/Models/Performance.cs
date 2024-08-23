namespace TheatricalPlayersRefactoringKata.Models
{
    public class Performance
    {
        public int Id { get; set; } 
        public int PlayId { get; set; }
        public Play Play { get; set; }
        public int Seats { get; set; }

        public Performance(int playId, int seats)
        {
            PlayId = playId;
            Seats = seats;
        }


        public Performance() { }
    }
}
