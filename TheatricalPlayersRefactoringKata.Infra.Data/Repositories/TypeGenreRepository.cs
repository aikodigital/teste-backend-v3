using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Infra.Data.Context;

namespace TheatricalPlayersRefactoringKata.Infra.Data.Repositories;

public class TypeGenreRepository : BaseRepository<TypeGenre, ApplicationDbContext>, ITypeGenreRepository
{
    public TypeGenreRepository(ApplicationDbContext context) : base(context)
    {

    }
}
