using TheatricalPlayersRefactoringKata.Domain.Models.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories.Interfaces;

public interface IRepository<T> where T : IAggregateRoot {}