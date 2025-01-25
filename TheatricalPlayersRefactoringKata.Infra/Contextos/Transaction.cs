using Microsoft.EntityFrameworkCore.Storage;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;


namespace TheatricalPlayersRefactoringKata.Infra.Contexto
{

    public class Transaction : ITransaction
    {
        private readonly AppDbContext _context;

        public Transaction(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

    }

}