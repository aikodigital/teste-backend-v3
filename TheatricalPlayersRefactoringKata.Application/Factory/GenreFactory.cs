using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Genres;
using TheatricalPlayersRefactoringKata.Domain.Enum;
namespace TheatricalPlayersRefactoringKata.Application.Factory;

public static class GenreFactory
{
    public static Play CreatePlay(string name, int lines, EnumGenres type)
    {
        switch (type)
        {
            case EnumGenres.Comedy:
                return new Comedy(name, lines, type);
            case EnumGenres.Tragedy:
                return new Tragedy(name, lines, type);
            case EnumGenres.History:
                return new History(name, lines, type);
            default:
                throw new ArgumentException("Invalid genre type.");
        }
    }
}
