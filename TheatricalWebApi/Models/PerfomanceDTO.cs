// PerformanceDTO.cs


namespace TheatricalWebApi.Models;
public class PerformanceDTO
{
    public PlayDTO Play { get; set; }
    public int Audience { get; set; }

    // Construtor padrão para desserialização
    public PerformanceDTO() { }

    // Construtor com parâmetros
    public PerformanceDTO(PlayDTO play, int audience)
    {
        Play = play;
        Audience = audience;
    }
}

