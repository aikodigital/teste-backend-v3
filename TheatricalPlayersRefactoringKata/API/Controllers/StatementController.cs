using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.API.Models;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly Func<string, IStatementGenerator> _statementGeneratorFactory;
        private readonly ILogger<StatementController> _logger;

        public StatementController(Func<string, IStatementGenerator> statementGeneratorFactory, ILogger<StatementController> logger)
        {
            _statementGeneratorFactory = statementGeneratorFactory;
            _logger = logger;
        }

        [HttpPost("text")]
        public IActionResult GenerateTextStatement([FromBody] StatementRequest request)
        {
            try
            {
                AssociatePlayIds(request);
                return GenerateStatement("text", request, "statement.txt");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar extrato.");
                return StatusCode(500, "Erro ao gerar extrato. Tente novamente mais tarde.");
            }
        }

        [HttpPost("xml")]
        public IActionResult GenerateXmlStatement([FromBody] StatementRequest request)
        {
            try
            {
                AssociatePlayIds(request);
                return GenerateStatement("xml", request, "statement.xml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar extrato.");
                return StatusCode(500, "Erro ao gerar extrato. Tente novamente mais tarde.");
            }
        }

        private void AssociatePlayIds(StatementRequest request)
        {
            var playDictionary = request.Plays.ToDictionary(p => p.Type, p => p.PlayId);

            foreach (var performance in request.Invoice.Performances)
            {
                if (playDictionary.TryGetValue(performance.Genre, out var playId))
                {
                    performance.PlayId = playId;
                }
                else
                {
                    throw new InvalidOperationException($"Play not found for genre: {performance.Genre}");
                }
            }
        }

        private IActionResult GenerateStatement(string format, StatementRequest request, string fileName)
        {
            try
            {
                if (request == null || request.Invoice == null || request.Plays == null)
                {
                    return BadRequest("Request, Invoice, ou Plays não podem ser nulos.");
                }

                var generator = _statementGeneratorFactory(format);
                var statement = generator.Generate(request.Invoice, request.Plays);

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\TheatricalPlayersRefactoringKata\\arquivos", format);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (format == "xml")
                {
                    var xmlSerializer = new XmlSerializer(typeof(StatementRequest));
                    using (var writer = new StreamWriter(Path.Combine(filePath, fileName)))
                    {
                        xmlSerializer.Serialize(writer, request);
                    }
                }
                else if (format == "text")
                {
                    System.IO.File.WriteAllText(Path.Combine(filePath, fileName), statement);
                }

                var fileContent = System.IO.File.ReadAllBytes(Path.Combine(filePath, fileName));
                return File(fileContent, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar extrato.");
                return StatusCode(500, "Erro ao gerar extrato. Tente novamente mais tarde.");
            }
        }
    }
}