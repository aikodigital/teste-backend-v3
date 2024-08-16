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
    
    public static List<PerformanceResponseModel> GeneratePerfResponse(List<PerformanceModel> performances){
        var performanceResponseList = new List<PerformanceResponseModel>();
        foreach (var model in performances){
            var performanceResponseModel = new PerformanceResponseModel {
                InvoiceModelId = model.InvoiceModelId,
                PlayGenre = model.PlayGenre,
                Audience = model.Audience,
                PlayId = model.PlayId,
            };
            performanceResponseList.Add(performanceResponseModel);
        }
        return performanceResponseList;
    }
}