using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Repository.Interfaces;

namespace TheatricalPlayersRefactoringKata.Repository;

public class PerfomanceRepository : IRepository<Performance>
{
    private readonly RepositoryContext _context;

    public PerfomanceRepository(RepositoryContext context)
    {
        _context = context;
    }

    public bool Add(Performance entity)
    {
        try
        {
            _context.Performances.Add(entity);
            _context.SaveChanges();
            return true;
        }
        catch(Exception e)
        {
            Console.Write(e);
            return false;
        }
    }

    public List<Performance> GetAll()
    {
        return _context.Performances.ToList();
    }

    public Performance? GetById(int id)
    {
        if (_context.Performances != null)
        {
            return _context.Performances?.FirstOrDefault(p => p.Id == id);
        }

        return null;
    }

    public bool Remove(int id)
    {
        var performance = GetById(id);

        if (performance != null)
        {
            _context.Performances.Remove(performance);
            _context.SaveChanges();

            return true;
        }

        return false;
    }

    public bool Update(int id, Performance entity)
    {
        var performance = _context.Performances?.FirstOrDefault(p => p.Id == id);

        if (performance != null)
        {
            performance.PlayId = entity.PlayId;
            performance.AmountOwed = entity.AmountOwed;
            performance.EarnedCredits = entity.EarnedCredits;
            performance.Audience = entity.Audience;

            _context.Entry(performance).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChangesAsync();

            return true;
        }

        return false;
    }
}
