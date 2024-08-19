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
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience), "Audience cannot be negative.");
            if (lines < 0) throw new ArgumentOutOfRangeException(nameof(lines), "Lines cannot be negative.");
            Play = play ?? throw new ArgumentNullException(nameof(play), "Play object cannot be null.");
            Price = price;

            _audience = audience;
            _lines = lines;
            PlayId = play.PlayId;
        }

        public void UpdateAudience(int audience)
        {
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience), "Audience cannot be negative.");
            _audience = audience;
        }

        public void UpdateLines(int lines)
        {
            if (lines < 0) throw new ArgumentOutOfRangeException(nameof(lines), "Lines cannot be negative.");
            _lines = lines;
        }

        public void UpdatePlayId(Guid playId)
        {
            PlayId = playId;
        }
    }
}