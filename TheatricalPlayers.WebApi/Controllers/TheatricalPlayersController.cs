using Application.UseCases.StatementUseCase;
using Domain.Contracts.UseCases.StatementUseCase;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using TheatricalPlayers.WebApi.Models;

namespace TheatricalPlayers.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatricalPlayersController : ControllerBase
    {
        private readonly IStatementPrinterUseCase _statementPrinterUseCase;

        public TheatricalPlayersController(IStatementPrinterUseCase statementPrinterUseCase)
        {
            _statementPrinterUseCase = statementPrinterUseCase;
        }

        [HttpPost("print")]
        public IActionResult PrintStatement([FromBody] PrintRequest request)
        {
            if (request == null || request.Invoice == null || request.Plays == null || request.Plays.Count == 0)
                return BadRequest("Invalid request");

            var result = _statementPrinterUseCase.Print(request.Invoice, request.Plays);
            return Content(result, "application/json");
        }

    }
}
