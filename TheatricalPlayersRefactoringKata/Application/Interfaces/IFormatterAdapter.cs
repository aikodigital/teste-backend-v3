using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces;

public interface IFormatterAdapter
{
    // TODO: Essa interface poderia trabalhar com diferentes tipos de objetos
    string Format(Statement statement);
}