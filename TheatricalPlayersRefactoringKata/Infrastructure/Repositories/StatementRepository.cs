using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

public class StatementRepository : IStatementRepository
{
    private readonly ApplicationDbContext _context;

    public StatementRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Statement>> GetAllStatementsAsync()
    {
        return await _context.Set<Statement>().Include(e => e.Items).ToListAsync();
    }

    public async Task AddStatementAsync(Statement statement)
    {
        await _context.Set<Statement>().AddAsync(statement);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}