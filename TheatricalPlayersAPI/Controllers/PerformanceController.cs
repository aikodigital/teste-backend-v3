using System.Net;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersAPI.Services;

namespace TheatricalPlayersAPI.Controllers;

[Route("performance")]
[ApiController]
public class PerformanceController : ControllerBase
{
    private readonly PerformanceServices _performanceServices;
	
    public PerformanceController(PerformanceServices performanceServices){
        _performanceServices = performanceServices;
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<PerformanceModel>>> Create(PerformanceModel request){
        var performance = await _performanceServices.Create(request);
        if (performance.statusCode == HttpStatusCode.BadRequest) return BadRequest(performance);
        if (performance.statusCode == HttpStatusCode.NotFound) return NotFound(performance);
        return Created("", performance);
    }

    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<PerformanceModel>>> Update(int id, PerformanceModel updatedPerformance)
    {
        var performance = await _performanceServices.Update(id, updatedPerformance);
        if(performance.statusCode == HttpStatusCode.BadRequest) return BadRequest(performance);
        if (performance.statusCode == HttpStatusCode.NotFound) return NotFound(performance);
        return Ok(performance);
    }

    [HttpGet("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<List<PerformanceModel>>>> GetAll(){
        var performances = await _performanceServices.GetAll();
        if (performances.statusCode == HttpStatusCode.NotFound) return NotFound(performances);
        return Ok(performances);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<PerformanceModel>>> GetById(int id){
        var performance = await _performanceServices.GetById(id);
        if(performance.statusCode == HttpStatusCode.NotFound) return NotFound(performance);
        return Ok(performance);
    }
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<PerformanceModel>>> Delete(int id)
    {
        var play = await _performanceServices.Delete(id);
        if(play.statusCode == HttpStatusCode.NotFound) return NotFound(play);
        return Ok(play);
    }
    
}