using Domain.DTOs;

namespace TheatricalPlayersRefactoringKata;

public class PerformanceModelView
{

    public int PlayId { get ; set ; }
    public int Audience { get; set; }

    public PerformanceModelView()
    {
  
    }

    public PerformanceDTO DTO()
    {
        var dto = new PerformanceDTO
        {
            PlayId = PlayId,
            Audience = Audience
        };

        return dto;
    }
}
