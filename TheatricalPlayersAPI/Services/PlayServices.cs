using System.Net;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;

namespace TheatricalPlayersAPI.Services;

public class PlayServices
{
    private readonly TheatricalDbContext _context;
    private ResponseModel<PlayModel> _response;
    
    public PlayServices(TheatricalDbContext context){
        _context = context;
        _response = new();
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
        
        _context.Add(play);
        await _context.SaveChangesAsync();

        _response.data = play;
        _response.message = "Play created successfully";
        _response.statusCode = HttpStatusCode.Created; 
        return _response;


    }
}