using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheatricalPlayersRefactoringKata.Core.Services;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Infrastructure;
using System;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementGenerator _textStatementGenerator;
        private readonly IStatementGenerator _xmlStatementGenerator;
        private readonly IEnumerable<IPerformanceCalculator> _calculators;
        private readonly ILogger<StatementController> _logger;


        public StatementController(
            IStatementGenerator textStatementGenerator,
            IStatementGenerator xmlStatementGenerator,
            IEnumerable<IPlayTypeCalculator> playTypeCalculators,
            ILogger<StatementController> logger)

        {
            _textStatementGenerator = textStatementGenerator;
            _xmlStatementGenerator = xmlStatementGenerator;
            _logger = logger;

            _calculators = playTypeCalculators
                .Select(ptc => new PlayTypeToPerformanceAdapter(ptc))
                .ToList();
        }

        [HttpPost("text")]
        public ActionResult<string> GenerateTextStatement([FromBody] StatementRequest request)
        {
            var statement = _textStatementGenerator.Generate(request.Invoice, request.Plays);

            var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\text", "statement.txt");

            System.IO.File.WriteAllText(filePath, statement);

            return Ok(statement);
        }

        [HttpPost("xml")]
        public ActionResult GenerateXmlStatement([FromBody] StatementRequest request)
        {
            if (request == null || request.Invoice == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var statement = _xmlStatementGenerator.Generate(request.Invoice, request.Plays);
                _logger.LogInformation($"Statement generated: {statement}");

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\xml", "statement.xml");

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                System.IO.File.WriteAllText(filePath, statement);

                var fileContent = System.IO.File.ReadAllBytes(filePath);

                return File(fileContent, "application/xml", "statement.xml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o extrato em XML.");
                return StatusCode(500, "Erro ao gerar o extrato em XML.");
            }
        }
    }
}