using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using TheatricalPlayersRefactoringKata;
using Api.Data;

namespace Api.Controllers;

// Run the API in directory src/API/. with [dotnet run]
// .NET 8.0 Version
[ApiController]
[Route("api")]
public class PrinterController : ControllerBase
{
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        var statements = await context.Statements.AsNoTracking().ToListAsync();

        return Ok(statements);
    }

    [HttpGet]
    [Route("all/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context,[FromRoute] int id)
    {
        var statement = await context.Statements.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return statement == null ? NotFound() : Ok(statement.Details);
    }

    [HttpPost]
    [Route("all/add")]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context)    
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        var statement = new Statement
        {
            Details = result
        };

        try
        {
            await context.Statements.AddAsync(statement);
            await context.SaveChangesAsync();
            return Created($"api/all/{statement.Id}", statement.Details);
        }
        catch (System.Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("all/delete/{id}")]
     public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context,[FromRoute] int id)
    {
        var statement = await context.Statements.FirstOrDefaultAsync(x => x.Id == id);

        try
        {
            context.Statements.Remove(statement);
            context.SaveChangesAsync();
            return Ok($"STATEMENT DELETED:\n {statement.Details}");
        }
        catch (System.Exception)
        {
            return StatusCode(500);
        }
    }
}
