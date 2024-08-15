using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Infrastructure;
using TheatricalPlayersRefactoringKata.API.Models;
using System.IO;
using System;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementGenerator _textStatementGenerator;
        private readonly IStatementGenerator _xmlStatementGenerator;
        private readonly ILogger<StatementController> _logger;
        private readonly IEnumerable<IPlayTypeCalculator> _calculators;

        public StatementController(
            IStatementGenerator textStatementGenerator,
            IEnumerable<IPlayTypeCalculator> calculators,
            ILogger<StatementController> logger)
        {
            _textStatementGenerator = textStatementGenerator;
            _calculators = calculators;
            _xmlStatementGenerator = new XmlStatementGenerator(calculators);
            _logger = logger;
        }

        [HttpPost("text")]
        public ActionResult<string> GenerateTextStatement([FromBody] StatementRequest request)
        {
            try
            {
                var statement = _textStatementGenerator.Generate(request.Invoice, request.Plays);

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\text", "statement.txt");
                System.IO.File.WriteAllText(filePath, statement);

                return Ok(statement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o extrato em texto.");
                return StatusCode(500, "Ocorreu um erro ao gerar o extrato em texto. Por favor, tente novamente mais tarde.");
            }
        }

        [HttpPost("xml")]
        public ActionResult<string> GenerateXmlStatement([FromBody] StatementRequest request)
        {
            try
            {
                var statement = _xmlStatementGenerator.Generate(request.Invoice, request.Plays);

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\xml", "statement.xml");
                System.IO.File.WriteAllText(filePath, statement);

                return Ok(statement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o extrato em XML.");
                return StatusCode(500, "Ocorreu um erro ao gerar o extrato em XML. Por favor, tente novamente mais tarde.");
            }
        }
    }
}