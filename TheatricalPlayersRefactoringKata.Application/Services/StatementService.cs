using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Services
{
    public class StatementService : IStatementService
    {
        private readonly IStatementRepository _repository;

        public StatementService(IStatementRepository repository)
        {
            _repository = repository;
        }
        public async Task<Statement> CreateStatementAsync(Statement statement)
        {
            return await _repository.AddAsync(statement);
        }
    }
}
