using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Models;

public class PerformanceModel
{
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public PerformanceModel(string playId, int audience)
    {
        _playId = playId;
        _audience = audience;
    }

    public static List<Performance> ConvertToEntity(IEnumerable<PerformanceModel> performanceModel)
    {
        return performanceModel.ToList().ConvertAll(performance => new Performance
        {
            PlayId = performance.PlayId,
            Audience = performance.Audience
        });
    }

    public static List<PerformanceModel> ConvertToModel(IEnumerable<Performance> performance)
    {
        return performance.ToList().ConvertAll(performance => new PerformanceModel(performance.PlayId, performance.Audience));
    }
}
