using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Performance
    {
        public Guid Id { get; set; }
        public Guid PlayId { get; set; }
        public Play Play { get; set; }
        public int Audience { get; set; }
        public int Credits { get; set; }

        public List<Invoice> Invoices { get; set; }
        
        public Performance()
        {
        }

        public Performance(Guid playId, Play play, int audience)
        {
            PlayId = play.Id;
            Play = play;
            Audience = audience;
        }

        public int CalculateVolumeCredits(Genre genre)
        {
            Credits = Math.Max(Audience - 30, 0);

            if (genre == Genre.Comedy)
            {
                Credits += (int)Math.Floor((decimal)Audience / 5);
            }

            return Credits;
        }
    }
}