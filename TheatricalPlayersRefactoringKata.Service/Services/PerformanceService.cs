using TheatricalPlayersRefactoringKata.Model.Models;
using TheatricalPlayersRefactoringKata.Repository;

namespace TheatricalPlayersRefactoringKata.Service;

public class PerformanceService
{
    private readonly PerfomanceRepository _performanceRepository;

    public PerformanceService(PerfomanceRepository performanceRepository)
    {
        _performanceRepository = performanceRepository;
    }

    public List<Performance> GetAllPerformances()
    {
        return _performanceRepository.GetAll();
    }

    public bool AddPerformance(Performance perf)
    {
        return _performanceRepository.Add(perf);
    }

    public Performance? GetPerformanceById(int id)
    {
        return _performanceRepository.GetById(id);
    }

    public bool DeletePerformance(int id)
    {
        return _performanceRepository.Remove(id);
    }

    public bool UpdatePerformance(int id, Performance perf)
    {
        return _performanceRepository.Update(id, perf);
    }
}
