using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.API.Models;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Data.Dto;

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

                var xmlInvoice = MapToXmlInvoice(request.Invoice);
                return GenerateStatement("xml", xmlInvoice, "statement.xml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar extrato.");
                return StatusCode(500, "Erro ao gerar extrato. Tente novamente mais tarde.");
            }
        }

        private XmlInvoice MapToXmlInvoice(Invoice invoice)
        {
            return new XmlInvoice
            {
                Customer = invoice.Customer,
                Performances = invoice.Performances.Select(p => new XmlPerformance
                {
                    PlayId = p.PlayId,
                    Audience = p.Audience
                }).ToList()
            };
        }

        private void AssociatePlayIds(StatementRequest request)
        {
            var playDictionary = request.Plays.ToDictionary(p => p.Type, p => p.PlayId);

            foreach (var performance in request.Invoice.Performances)
            {
                if (playDictionary.TryGetValue(performance.Genre, out var playId))
                {
                    performance.UpdatePlayId(playId);
                }
                else
                {
                    throw new InvalidOperationException($"Play not found for genre: {performance.Genre}");
                }
            }
        }

        private IActionResult GenerateStatement(string format, object data, string fileName)
        {
            try
            {
                if (data == null)
                {
                    return BadRequest("Os dados não podem ser nulos.");
                }

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\TheatricalPlayersRefactoringKata\\arquivos", format);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (format == "xml")
                {
                    var xmlSerializer = new XmlSerializer(data.GetType());
                    using (var writer = new StreamWriter(Path.Combine(filePath, fileName)))
                    {
                        xmlSerializer.Serialize(writer, data);
                    }
                }
                else if (format == "text")
                {
                    var statement = data as string;
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