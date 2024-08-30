using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public class StatementService : IStatementService
{
    private readonly IStatementRepository _statementRepository;

    public StatementService(IStatementRepository statementRepository)
    {
        _statementRepository = statementRepository;
    }

    public async Task<IEnumerable<Statement>> GetAllStatementsAsync()
    {
        return await _statementRepository.GetAllStatementsAsync();
    }

    public async Task AddStatementAsync(Statement statement)
    {
        await _statementRepository.AddStatementAsync(statement);
        await _statementRepository.SaveChangesAsync();
    }
}
