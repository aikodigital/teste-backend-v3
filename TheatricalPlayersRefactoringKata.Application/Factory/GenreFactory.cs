using TheatricalPlayersRefactoringKata.Domain.Entity;
using TheatricalPlayersRefactoringKata.Application.Genres;
using TheatricalPlayersRefactoringKata.Domain.Enum;
namespace TheatricalPlayersRefactoringKata.Application.Factory;

public static class GenreFactory
{
    public static Domain.Entity.Play CreatePlay(string name, int lines, EnumGenres type)
    {
        switch (type)
        {
            case EnumGenres.Comedy:
                return new Comedy(name, lines);
            case EnumGenres.Tragedy:
                return new Tragedy(name, lines);
            case EnumGenres.History:
                return new History(name, lines);
            default:
                throw new ArgumentException("Invalid genre type.");
        }
    }
}
