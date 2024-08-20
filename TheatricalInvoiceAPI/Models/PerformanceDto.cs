

public class PerformanceDto
{
    public string PlayId { get; set; }
    public int Audience { get; set; }

    public PerformanceDto(string playId, int audience)
    {
        PlayId = playId ?? throw new ArgumentNullException(nameof(playId));
        Audience = audience;
    }
}