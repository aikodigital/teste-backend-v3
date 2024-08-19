using Microsoft.EntityFrameworkCore;

using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Common;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

namespace TheatricalPlayersRefactoringKata.Server.Database.Repositories
{
    public class InvoiceRepository : Repository<PlayEntity>
    {
        public InvoiceRepository(DbContextTheatricalPlayers context) : base(context)
        {
        }
    }
}