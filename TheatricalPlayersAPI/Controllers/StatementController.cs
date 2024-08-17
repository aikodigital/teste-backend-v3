using System.Net;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersAPI.Services;

namespace TheatricalPlayersAPI.Controllers;

[Route("statements")]
[ApiController]
public class StatementController : ControllerBase
{
    private readonly StatementServices _statementServices;
    
    public StatementController(StatementServices statement_services){
        _statementServices = statement_services;
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<StatementModel>>> Create(StatementModel request){
        var statement = await _statementServices.Create(request);
        if(statement.statusCode == HttpStatusCode.NotFound) return NotFound(statement);
        return Ok(statement);
    }
    
    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<StatementModel>>> Create(int id, StatementModel request){
        var statement = await _statementServices.Update(id, request);
        if(statement.statusCode == HttpStatusCode.NotFound) return NotFound(statement);
        return Ok(statement);
    }
    
    [HttpGet("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<StatementModel>>> GetAll(){
        var statement = await _statementServices.GetAll();
        if(statement.statusCode == HttpStatusCode.NotFound) return NotFound(statement);
        return Ok(statement);
    }
    
    [HttpGet("get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<StatementModel>>> GetById(int id){
        var statement = await _statementServices.GetById(id);
        if(statement.statusCode == HttpStatusCode.NotFound) return NotFound(statement);
        return Ok(statement);
    }
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<StatementModel>>> Delete(int id){
        var statement = await _statementServices.Delete(id);
        if(statement.statusCode == HttpStatusCode.NotFound) return NotFound(statement);
        return Ok(statement);
    }

    [HttpPost("generate/{invoiceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<StatementModel>>> Generate(int invoiceId, string printMode){
        var statement = await _statementServices.Generate(invoiceId, printMode);
        if(statement.statusCode == HttpStatusCode.BadRequest) return BadRequest(statement);
        if(statement.statusCode == HttpStatusCode.NotFound) return NotFound(statement);
        return Ok(statement);
    }


}