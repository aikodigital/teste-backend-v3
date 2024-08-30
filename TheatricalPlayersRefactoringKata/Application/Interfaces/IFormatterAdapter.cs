using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IFormatterAdapter
{
    string Format(Statement statement);
}