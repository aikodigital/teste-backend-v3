using System;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Performance
    {
        public Guid PlayId { get; set; }
        public Genre Genre { get; private set; }
        public int Audience { get; private set; }
        public int Lines { get; private set; }
        public Play Play { get; set; }

        public Performance(Genre genre, int audience, int lines, Play play = null)
        {
            Genre = genre;
            Audience = audience >= 0 ? audience : throw new ArgumentOutOfRangeException(nameof(audience));
            Lines = lines >= 0 ? lines : throw new ArgumentOutOfRangeException(nameof(lines));
            Play = play;
        }

        public void UpdateAudience(int audience)
        {
            if (audience < 0) throw new ArgumentOutOfRangeException(nameof(audience));
            Audience = audience;
        }
    }
}