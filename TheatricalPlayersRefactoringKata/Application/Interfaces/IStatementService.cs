using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IStatementService
{
    Task<IEnumerable<Statement>> GetAllStatementsAsync();
    Task AddStatementAsync(Statement statement);
}