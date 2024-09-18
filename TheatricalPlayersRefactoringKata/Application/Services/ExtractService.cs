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

    public async Task AddExtract(StatementResult statementResult)
    {
        _context.StatementResults.Add(statementResult);
        await _context.SaveChangesAsync();
    }
}
