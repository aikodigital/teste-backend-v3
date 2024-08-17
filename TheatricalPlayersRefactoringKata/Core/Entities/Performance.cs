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

        public Performance(Genre genre, int audience, int lines, Play play)
        {
            Genre = genre;
            _audience = audience >= 0 ? audience : throw new ArgumentOutOfRangeException(nameof(audience));
            _lines = lines >= 0 ? lines : throw new ArgumentOutOfRangeException(nameof(lines));
            Play = play ?? throw new ArgumentNullException(nameof(play));
            PlayId = play.PlayId;
        }

        public void UpdateAudience(int audience)
        {
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience));
            _audience = audience;
        }

        public void UpdateLines(int lines)
        {
            if (lines < 0) throw new ArgumentOutOfRangeException(nameof(lines));
            _lines = lines;
        }

        public void UpdatePlayId(Guid playId)
        {
            PlayId = playId;
        }
    }
}