using Domain.Contracts.Repositories;
using Domain.Entities;
using Infra.Context;
using Infra.Repository.Repositories;

namespace Infra.Repositories
{
    public class StatementItemRepository : BaseRepository<Item>, IStatementItemRepository
    {
        public StatementItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
