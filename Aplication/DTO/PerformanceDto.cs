namespace Aplication.DTO
{
    public class PerformanceDto
    {
        public PlayDto Play { get; set; }
        public int Audience { get; set; }

        public PerformanceDto(PlayDto play, int audience)
        {
            Play = play;
            Audience = audience;
        }

    }
}
