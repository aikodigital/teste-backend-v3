using Domain.Contracts.Repositories.PlayRepository;
using Domain.Entities;
using Infra.Context;

namespace Infra.Repository.Repositories.PlayRepository
{
    public class PlayRepository : BaseRepository<Play>, IPlayRepository
    {
        public PlayRepository(AppDbContext context) : base(context)
        {
        }
    }
}
