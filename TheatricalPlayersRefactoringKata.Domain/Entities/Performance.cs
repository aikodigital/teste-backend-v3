using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Performance
    {
        public string PlayId { get; set; }
        public int Audience { get; set; }
        public int Credits { get; set; }


        public Performance(string playId, int audience)
        {
            PlayId = playId;
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