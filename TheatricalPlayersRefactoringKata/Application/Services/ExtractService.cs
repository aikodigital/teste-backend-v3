using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class ExtractService
{
    private readonly ApplicationDbContext _context;

    public ExtractService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddExtract(StatementResult statementResult)
    {
        _context.StatementResults.Add(statementResult);
        _context.SaveChanges();
    }
}
