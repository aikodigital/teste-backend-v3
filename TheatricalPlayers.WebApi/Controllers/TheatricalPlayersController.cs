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
        private readonly IStatementUseCase _statementUserCase;
        private readonly IStatementItemUseCase _itemUseCase;
        private readonly IConvertUseCase _convertUseCase;

        public TheatricalPlayersController(IStatementPrinterUseCase statementPrinterUseCase, IStatementUseCase statementUserCase, IConvertUseCase convertUseCase,
            IStatementItemUseCase itemUseCase)
        {
            _statementPrinterUseCase = statementPrinterUseCase;
            _statementUserCase = statementUserCase;
            _convertUseCase = convertUseCase;
            _itemUseCase = itemUseCase;
        }

        [HttpPost("print")]
        public IActionResult PrintStatement([FromBody] PrintRequest request)
        {
            if (request == null || request.Invoice == null || request.Plays == null || request.Plays.Count == 0)
                return BadRequest("Invalid request");

            var result = _statementPrinterUseCase.Print(request.Invoice, request.Plays);

            Statement statement = JsonConvert.DeserializeObject<Statement>(result);
            _statementUserCase.CreateStatement(statement);

            return Content(result, "application/json");
        }

        [HttpPost("bringXML")]
        public IActionResult PrintStatementBringXML([FromBody] PrintRequest request)
        {
            if (request == null || request.Invoice == null || request.Plays == null || request.Plays.Count == 0)
                return BadRequest("Invalid request");

            var result = _statementPrinterUseCase.Print(request.Invoice, request.Plays);

            var xmlOutput = _convertUseCase.ConvertJsonToXml(result);

            Statement statement = JsonConvert.DeserializeObject<Statement>(result);
            _statementUserCase.CreateStatement(statement);

            var xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlOutput);
            return File(xmlBytes, "application/xml", "statement.xml");
        }

        // Update Statement
        [HttpPut("{id}")]
        public IActionResult UpdateStatement(int id, [FromBody] Statement statement)
        {
            if (statement == null || statement.Id != id)
                return BadRequest("Statement data is invalid");

            var existingStatement = _statementUserCase.GetByIdStatement(id);
            if (existingStatement == null)
                return NotFound();

            _statementUserCase.UpdateStatement(statement);
            return NoContent();
        }

        // Delete Statement
        [HttpDelete("{id}")]
        public IActionResult DeleteStatement(int id)
        {
            var statement = _statementUserCase.GetByIdStatement(id);
            if (statement == null)
                return NotFound();

            _statementUserCase.DeleteStatement(statement);
            return NoContent();
        }

        // Get Statement by Id
        [HttpGet("{id}")]
        public IActionResult GetByIdStatement(int id)
        {
            var statement = _statementUserCase.GetByIdStatement(id);
            statement.Items = _itemUseCase.GetAllStatementItemByIdStatement(id);

            if (statement == null)
                return NotFound();

            return Ok(statement);
        }

        // Get All Statements
        [HttpGet]
        public IActionResult GetAllStatements()
        {
            var statements = _statementUserCase.GetAllStatement();

            var allItens = _itemUseCase.GetAll();

            foreach (var statement in statements)
            {
                statement.Items = allItens.Where(w => w.StatementId == statement.Id).ToList();
            }
            return Ok(statements);
        }
    }
}
