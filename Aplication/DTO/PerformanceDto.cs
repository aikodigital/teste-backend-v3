using CrossCutting;

namespace Aplication.DTO
{
    public class PerformanceDto
    {
        public PerformanceDto() { }
        public PlayDto Play { get; set; }
        public int Audience { get; set; }

        public PlayType PlayType => Play.Type;

        public PerformanceDto(PlayDto play, int audience)
        {
            Play = play;
            Audience = audience;
        }

    }
}
