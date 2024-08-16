using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersAPI.Services;

namespace TheatricalPlayersAPI.Controllers;

[Route("play")]
[ApiController]
public class PlayController : ControllerBase
{
	private readonly PlayServices _playServices;
	
	public PlayController(PlayServices playServices){
		_playServices = playServices;
	}

	[HttpPost("create")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<Response<PlayModel>>> Create(PlayModel request)
	{
		var play = await _playServices.Create(request);
		if (play.statusCode == HttpStatusCode.BadRequest) return BadRequest(play);
		return Ok(play);
	}

	[HttpPut("update/{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Response<PlayModel>>> Update(int id, PlayModel request)
	{
		var play = await _playServices.Update(id, request);
		if (play.statusCode == HttpStatusCode.BadRequest) return BadRequest(play);
		if (play.statusCode == HttpStatusCode.NotFound) return NotFound(play);
		return Ok(play);
	}

	[HttpGet("getAll")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Response<List<PlayModel>>>> GetAll()
	{
		var plays = await _playServices.GetAll();
		if(plays.statusCode == HttpStatusCode.NotFound) return NotFound(plays);
		return Ok(plays);
	}

	[HttpGet("name/{name}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Response<PlayModel>>> GetByName(string name){
		var play = await _playServices.GetByName(name);
		if(play.statusCode == HttpStatusCode.NotFound) return NotFound(play);
		return Ok(play);
	}
	
	[HttpGet("genre/{genre}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Response<List<PlayModel>>>> GetByGenre(string genre)
	{
		var play = await _playServices.GetByGenre(genre);
		if(play.statusCode == HttpStatusCode.NotFound) return NotFound(play);
		return Ok(play);
	}

	[HttpDelete("delete/{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<Response<PlayModel>>> Delete(int id)
	{
		var play = await _playServices.Delete(id);
		if(play.statusCode == HttpStatusCode.NotFound) return NotFound(play);
		return Ok(play);
	}
}