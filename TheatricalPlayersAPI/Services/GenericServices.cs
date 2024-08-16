using TheatricalPlayersAPI.Models;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersAPI.Services;

public class GenericServices
{
    public static bool validateGenre(string genre)
    {
        if (genre == ""){
            return false;
        }
        if (genre != Genres.COMEDY &&
            genre != Genres.TRAGEDY &&
            genre != Genres.HISTORY){
            return false;
        }
        return true;
    }

    public static bool validateName(string name){
        if (name == "") return false;
        return true;
    }
}