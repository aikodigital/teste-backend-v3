using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersAPI.Services;

namespace TheatricalPlayersAPI.Controllers;

public class PlayController : ControllerBase
{
	private ResponseModel<PlayModel> _response;
	private PlayServices _playServices;
	private readonly TheatricalDbContext _context;
	
	public PlayController(){
		_context = new(new DbContextOptions<TheatricalDbContext>());
		_response = new();
		_playServices = new(_context);
	}

	[HttpPost("create")]
	public async Task<ActionResult<ResponseModel<PlayModel>>> Create(PlayModel request){
		
		var play = await _playServices.Create(request);
		if (play.statusCode == HttpStatusCode.BadRequest) return BadRequest(play);
		return Ok(play);
	}
}