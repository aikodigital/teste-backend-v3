using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Infra.DataBase.Repository
{
    public class PerformanceRepository
    {
        private readonly TheatricalContext _context;

        public PerformanceRepository(IDbContextFactory<TheatricalContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public void CreatePerformance(Performance performance)
        {
            _context.Performances.Add(performance);
            _context.SaveChanges();
        }

        public void UpdatePerformance(Performance performance)
        {
            var oldPerformance = _context.Performances.FirstOrDefault(x => x.Id == performance.Id);
            if (oldPerformance != null)
            {
                oldPerformance.Audience = performance.Audience;
                oldPerformance.Play = performance.Play;
                oldPerformance.InvoiceId = performance.InvoiceId;
                oldPerformance.PlayId = performance.PlayId;

                _context.Performances.Update(oldPerformance);
                _context.SaveChanges();
            }
        }

        public void DeletePerformance(int id)
        {
            var performanceToRemove = _context.Performances.FirstOrDefault(x => x.Id == id);
            if (performanceToRemove != null)
            {
                _context.Performances.Remove(performanceToRemove);
                _context.SaveChanges();
            }
        }

        public Performance? GetPerformanceById(int id)
        {
            return _context.Performances
                .Include(p => p.Play)
                .FirstOrDefault(x => x.Id == id);
        }
        public List<Performance> GetAllPerformances()
        {
            return _context.Performances
                .Include(p => p.Play)
                .AsNoTracking()
                .ToList();
        }
    }
}
