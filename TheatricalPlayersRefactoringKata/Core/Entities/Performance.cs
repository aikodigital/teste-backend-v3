using System;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Performance
    {
        private int _audience;
        private int _lines;
        public Guid PlayId { get; private set; }
        public Genre Genre { get; private set; }
        public int Audience => _audience;
        public int Lines => _lines;
        public Play Play { get; private set; }
        public decimal Price { get; set; }

        public Performance() { }

        public Performance(Genre genre, int audience, int lines, Play play, decimal price)
        {
            Genre = genre;
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience), "O Audience não pode ser negativo.");
            if (lines < 1000 || lines > 4000)
                throw new ArgumentOutOfRangeException(nameof(lines), "O número de linhas deve estar entre 1000 e 4000.");
            Play = play ?? throw new ArgumentNullException(nameof(play), "Play não pode ser nulo.");
            Price = price;

            _audience = audience;
            _lines = lines;
            PlayId = play.PlayId;
        }

        public void UpdateAudience(int audience)
        {
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience), "Audience não pode ser negativo.");
            _audience = audience;
        }

        public void UpdateLines(int lines)
        {
            if (lines < 1000 || lines > 4000)
                throw new ArgumentOutOfRangeException(nameof(lines), "O número de linhas deve estar entre 1000 e 4000.");
            _lines = lines;
        }

        public void UpdatePlayId(Guid playId)
        {
            PlayId = playId;
        }
    }
}