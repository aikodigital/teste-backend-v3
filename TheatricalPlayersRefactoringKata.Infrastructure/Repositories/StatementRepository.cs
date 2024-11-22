using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories
{
    public class StatementRepository : IStatementRepository
    {
        private readonly ApplicationDbContext _context;

        public StatementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Statement> AddAsync(Statement statement)
        {
            _context.Statements.Add(statement);
            await _context.SaveChangesAsync();
            return statement;
        }
    }
}
