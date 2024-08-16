using System.Net;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersAPI.Services;

public class PlayServices
{
    private readonly TheatricalDbContext _context;
    private Response<PlayModel> _response;
    private Response<List<PlayModel>> _responseList;
    
    public PlayServices(TheatricalDbContext context){
        _context = context;
        _response = new();
        _responseList = new();
    }
    public async Task<Response<PlayModel>> Create(PlayModel request){
        
        if (request == null){
            _response.message = "Request is empty";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        var play = new PlayModel();
        play.Name = request.Name;
        play.Genre = request.Genre.ToLower();
        play.Lines = request.Lines;

        var is_name_valid = GenericServices.validateName(play.Name);
        if (!is_name_valid){
            _response.data = play;
            _response.message = $"Play name is invalid";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        var is_genre_valid = GenericServices.validateGenre(play.Genre);
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
    
    public async Task<Response<PlayModel>> Update(int id, PlayModel request){
        
        var play = await _context.Plays.FirstOrDefaultAsync(play => play.Id == id);
        if (play == null){
            _response.message = "Play doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        play.Name = request.Name;
        play.Genre = request.Genre.ToLower();
        play.Lines = request.Lines;
        
        var is_name_valid = GenericServices.validateName(play.Name);
        if (!is_name_valid){
            _response.data = play;
            _response.message = $"Play name is invalid";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        var is_genre_valid = GenericServices.validateGenre(play.Genre);
        if (!is_genre_valid){
            _response.data = play;
            _response.message = $"Genre are invalid. Should be {Genres.COMEDY}, " +
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

    public async Task<Response<List<PlayModel>>> GetAll(){
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
    
    public async Task<Response<PlayModel>> GetByName(string playName)
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
    
    public async Task<Response<List<PlayModel>>> GetByGenre(string genre)
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

    public async Task<Response<PlayModel>>  Delete(int id)
    {
        var play = _context.Plays.FirstOrDefault(play => play.Id == id);
        if (play == null){
            _response.message = "Selected play does not exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;   
        }
        
        _context.Remove(play);
        await _context.SaveChangesAsync();
        
        _response.data = play;
        _response.message = "Selected play deleted successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }
}
