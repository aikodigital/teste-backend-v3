using System.Net;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersAPI.Services;

public class PlayServices
{
    private readonly TheatricalDbContext _context;
    private ResponseModel<PlayModel> _response;
    private ResponseModel<List<PlayModel>> _responseList;
    
    public PlayServices(TheatricalDbContext context){
        _context = context;
        _response = new();
        _responseList = new();
    }
    public async Task<ResponseModel<PlayModel>> Create(PlayModel? request){
        
        if (request == null){
            _response.message = "Request is empty";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        var play = new PlayModel();
        play.Name = request.Name;
        play.Genre = request.Genre;
        play.Lines = request.Lines;
        
        var is_genre_valid = validateGenre(play);
        if (!is_genre_valid){
            _response.data = play;
            _response.message = $"Genre are invalid. Should be {Genres.COMEDY}, " +
                                $"{Genres.TRAGEDY} " +
                                $"or {Genres.HISTORY}";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        _context.Add(play);
        await _context.SaveChangesAsync();

        _response.data = play;
        _response.message = "Play created successfully";
        _response.statusCode = HttpStatusCode.Created; 
        return _response;
    }
    
    public async Task<ResponseModel<PlayModel>> Update(PlayModel? request){
        var play = new PlayModel();
        play.Name = request.Name;
        play.Genre = request.Genre;
        play.Lines = request.Lines;
        
        var is_genre_valid = validateGenre(play);
        if (!is_genre_valid){
            _response.message = $"Genre is invalid. Should be {Genres.COMEDY}, " +
                                $"{Genres.TRAGEDY} " +
                                $"or {Genres.HISTORY}";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        _context.Update(play);
        await _context.SaveChangesAsync();

        _response.data = play;
        _response.message = "Play updated successfully";
        _response.statusCode = HttpStatusCode.OK; 
        return _response;
    }

    public async Task<ResponseModel<List<PlayModel>>> GetAll(){
        var plays = await _context.Plays.ToListAsync();
        if (plays.Count == 0){
            _responseList.message = "No plays were found";
            _responseList.statusCode = HttpStatusCode.NotFound;
            return _responseList;
        }
        _responseList.data = plays;
        _responseList.message = "Plays retrieved successfully";
        _responseList.statusCode = HttpStatusCode.OK;
        return _responseList;
    }
    
    public async Task<ResponseModel<PlayModel>> GetByName(string playName)
    {
        var play = await _context.Plays.FirstOrDefaultAsync(play => play.Name == playName);
        if (play == null){
            _response.message = $"Play with name {playName} does not exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        _response.data = play;
        _response.message = $"Play with name {playName} retrieved successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }

    public bool validateGenre(PlayModel play)
    {
        if (play.Genre != Genres.COMEDY &&
            play.Genre != Genres.TRAGEDY &&
            play.Genre != Genres.HISTORY){
            return false;
        }
        return true;
    }


    public async Task<ResponseModel<List<PlayModel>>> GetByGenre(string genre)
    {
        var plays = _context.Plays.Where(play => play.Genre == genre).ToList();
        if (plays.Count == 0){
            _responseList.message = $"No plays with {genre} genre found";
            _responseList.statusCode = HttpStatusCode.NotFound;
            return _responseList;
        }
        
        _responseList.data = plays;
        _responseList.message = $"Plays with {genre} genre retrieved successfully";
        _responseList.statusCode = HttpStatusCode.OK;
        
        return _responseList;
    }

    public async Task<ResponseModel<PlayModel>>  Delete(string name)
    {
        var play = _context.Plays.FirstOrDefault(play => play.Name == name);
        if (play == null){
            _response.message = $"Play with name {name} does not exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;   
        }
        
        _context.Remove(play);
        await _context.SaveChangesAsync();
        
        _response.data = play;
        _response.message = $"Play {name} deleted successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }
}
