using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Context;
using Infra.Repository.Repositories;

namespace Infra.Repositories
{
    public class StatementRepository : BaseRepository<Statement>, IStatementRepository
    {
        public StatementRepository(AppDbContext context) : base(context)
        {
        }
    }
}
