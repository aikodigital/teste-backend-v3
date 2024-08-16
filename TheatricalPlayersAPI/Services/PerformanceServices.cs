using System.Net;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersAPI.Services;

public class PerformanceServices
{
    private readonly TheatricalDbContext _context;
    private Response<PerformanceResponseModel> _response;
    private Response<List<PerformanceResponseModel>> _responseList;
    
    public PerformanceServices(TheatricalDbContext context){
        _context = context;
        _response = new();
        _responseList = new();
    }

    public async Task<Response<PerformanceResponseModel>> Create(PerformanceModel request){
        var playServices = new PlayServices(_context);
        var result = await playServices.GetByName(request.PlayId);
        var play = result.data;
        if (play == null){
            _response.message = $"Play with name {request.PlayId} doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        var performance = new PerformanceModel {
            Audience = request.Audience,
            PlayId = play.Name,
            PlayGenre = play.Genre
        };
        var performanceResponse = new PerformanceResponseModel {
            Audience = performance.Audience,
            PlayId = performance.PlayId,
            PlayGenre = performance.PlayGenre,
            InvoiceModelId = performance.InvoiceModelId
        };
        _context.Performances.Add(performance);
        await _context.SaveChangesAsync();
        
        _response.data = performanceResponse;
        _response.message = "Performance created successfully";
        _response.statusCode = HttpStatusCode.Created; 
        return _response;
    }

    public async Task<Response<PerformanceResponseModel>> Update(int id, PerformanceModel request)
    {
        var performance = await _context.Performances.FirstOrDefaultAsync(perf => perf.Id == id);
        if (performance == null){
            _response.message = "Performance doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        var play = await _context.Plays.FirstOrDefaultAsync(playModel => playModel.Name == request.PlayId);
        if (play == null){
            _response.message = $"Play with name {request.PlayId} doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        performance.Audience = request.Audience;
        performance.PlayId = request.PlayId;
        performance.PlayGenre = play.Genre;
        
        _context.Performances.Update(performance);
        await _context.SaveChangesAsync();

        var performanceResponse = new PerformanceResponseModel {
            Audience = performance.Audience,
            PlayId = performance.PlayId,
            PlayGenre = performance.PlayGenre,
            InvoiceModelId = performance.InvoiceModelId
        };
        
        _response.data = performanceResponse;
        _response.message = "Performance updated successfully";
        _response.statusCode = HttpStatusCode.OK;
        return _response;
    }

    public async Task<Response<List<PerformanceResponseModel>>> GetAll(){
        var performances = await _context.Performances.ToListAsync();
        if (performances.Count == 0){
            _responseList.message = "No performances found";
            _responseList.statusCode = HttpStatusCode.NotFound;
            return _responseList;
        }
        
        var performancesResponse = new List<PerformanceResponseModel>();
        foreach (var model in performances){
            var performanceResponseModel = new PerformanceResponseModel {
                Audience = model.Audience,
                PlayGenre = model.PlayGenre,
                PlayId = model.PlayId,
                InvoiceModelId = model.InvoiceModelId
            };
            performancesResponse.Add(performanceResponseModel);
        }
        
        _responseList.data = performancesResponse;
        _responseList.message = "Retrieved all performances successfully";
        _responseList.statusCode = HttpStatusCode.OK;
        
        return _responseList;
    }

    public async Task<Response<PerformanceResponseModel>> GetById(int id){
        var performance = await _context.Performances.FirstOrDefaultAsync(perf => perf.Id == id);
        if (performance == null){
            _response.message = "Performance doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }

        var performanceResponse = new PerformanceResponseModel {
            Audience = performance.Audience,
            PlayId = performance.PlayId,
            PlayGenre = performance.PlayGenre,
            InvoiceModelId = performance.InvoiceModelId
        };
        
        _response.data = performanceResponse;
        _response.message = "Performance retrieved successfully";
        _response.statusCode = HttpStatusCode.OK;
        return _response;
    }

    public async Task<Response<PerformanceResponseModel>> Delete(int id){
        var performance = await _context.Performances.FirstOrDefaultAsync();
        if (performance == null){
            _response.message = "Performance doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        _context.Performances.Remove(performance);
        await _context.SaveChangesAsync();

        var performanceResponse = new PerformanceResponseModel {
            Audience = performance.Audience,
            PlayId = performance.PlayId,
            PlayGenre = performance.PlayGenre,
            InvoiceModelId = performance.InvoiceModelId
        };
    
        _response.data = performanceResponse;
        _response.message = "Performance deleted successfully";
        _response.statusCode = HttpStatusCode.OK;
        return _response;

    }
}