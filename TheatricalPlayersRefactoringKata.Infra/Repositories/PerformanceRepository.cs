using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Infra.Contexto;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Domain.Exception;
using TheatricalPlayersRefactoringKata.Infra.Interfaces.Repository;

namespace TheatricalPlayersRefactoringKata.Infra.Repositories
{
    public class PerformanceRepository : IPerformanceRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PerformanceRepository> _logger;

        public PerformanceRepository(AppDbContext context, ILogger<PerformanceRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PerformanceEntity> AddPerformance(PerformanceEntity performance)
        {
            try
            {
                await _context.Performance.AddAsync(performance);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Performance adicionada com sucesso.");
                return performance;
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<IEnumerable<PerformanceEntity>> ListPerformances()
        {
            try
            {
                return await _context.Performance.ToListAsync();
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<bool> DeletePerformance(long id)
        {
            try
            {
                var performance = await _context.Performance.FirstOrDefaultAsync(x => x.Id == id);

                if (performance == null)
                {
                    _logger.LogWarning($"Performance com ID {id} não encontrada para exclusão.");
                    return false;
                }

                _context.Performance.Remove(performance);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Performance com ID {id} removida com sucesso.");
                return true;
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<IEnumerable<PerformanceEntity>> GetPerformance(long id)
        {
            try
            {
                var performances = await _context.Performance
                    .Where(x => x.Id == id)
                    .ToListAsync();

                if (!performances.Any())
                {
                    _logger.LogWarning($"Nenhuma performance encontrada com o ID {id}.");
                }

                return performances;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<PerformanceEntity> UpdatePerformance(PerformanceEntity performance)
        {
            try
            {
                var performanceExistente = await _context.Performance.FirstOrDefaultAsync(x => x.Id == performance.Id);

                if (performanceExistente == null)
                {
                    _logger.LogWarning($"Performance com ID {performance.Id} não encontrada para atualização.");
                    return null;
                }

                _context.Entry(performanceExistente).CurrentValues.SetValues(performance);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Performance com ID {performance.Id} atualizada com sucesso.");
                return performance;
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                throw new BusinessException(ex.Message);
            }
        }
    }
}
