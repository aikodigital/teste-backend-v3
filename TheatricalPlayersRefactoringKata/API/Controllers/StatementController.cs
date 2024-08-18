using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheatricalPlayersRefactoringKata.API.Models;
using TheatricalPlayersRefactoringKata.Core.Entities;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Data.Dto;
using TheatricalPlayersRefactoringKata.Infrastructure.Configuration;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly Func<string, IStatementGenerator> _statementGeneratorFactory;
        private readonly ILogger<StatementController> _logger;
        private readonly FileSettings _fileSettings;
        private readonly Dictionary<string, IPerformanceCalculator> _calculators;
        private readonly Dictionary<string, Play> _plays;

        public StatementController(
             Func<string, IStatementGenerator> statementGeneratorFactory,
             ILogger<StatementController> logger,
             IOptions<FileSettings> fileSettings,
             Dictionary<string, IPerformanceCalculator> calculators,
             Dictionary<string, Play> plays)
        {
            _statementGeneratorFactory = statementGeneratorFactory;
            _logger = logger;
            _fileSettings = fileSettings.Value;
            _calculators = calculators;
            _plays = plays;
        }


        [HttpPost("text")]
        public ActionResult<string> GenerateTextStatement([FromBody] StatementRequest request)
        {
            if (request == null || request.Invoice == null)
            {
                _logger.LogError("Request data is missing.");
                return BadRequest("Invalid data.");
            }

            try
            {
                AssociatePlayIds(request);

                var playDictionary = request.Plays.ToDictionary(play => play.Value.PlayId, play => play.Value);

                var statementGenerator = _statementGeneratorFactory("text");
                var statement = statementGenerator.Generate(request.Invoice, playDictionary);

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\text", "statement.txt");

                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                System.IO.File.WriteAllText(filePath, statement);
                var fileContent = System.IO.File.ReadAllBytes(filePath);

                return File(fileContent, "text/plain", "statement.txt");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar extrato.");
                return StatusCode(500, "Erro ao gerar extrato. Tente novamente mais tarde.");
            }
        }

        [HttpPost("xml")]
        public ActionResult<string> GenerateXmlStatement([FromBody] StatementRequest request)
        {
            if (request == null || request.Invoice == null)
            {
                _logger.LogError("Request data is missing.");
                return BadRequest("Invalid data.");
            }

            try
            {
                AssociatePlayIds(request);

                var playDictionary = request.Plays.ToDictionary(play => play.Value.PlayId, play => play.Value);

                var statementGenerator = _statementGeneratorFactory("xml");
                var statement = statementGenerator.Generate(request.Invoice, playDictionary);

                var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\xml", "statement.xml");

                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                System.IO.File.WriteAllText(filePath, statement);
                var fileContent = System.IO.File.ReadAllBytes(filePath);

                return File(fileContent, "application/xml", "statement.xml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar extrato.");
                return StatusCode(500, "Erro ao gerar extrato. Tente novamente mais tarde.");
            }
        }

        private XmlInvoice MapToXmlInvoice(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            return new XmlInvoice
            {
                Customer = invoice.Customer,
                TotalAmount = invoice.TotalAmount,
                TotalCredits = invoice.TotalCredits,
                Performances = invoice.Performances.Select(p => new XmlPerformance
                {
                    PlayId = p.PlayId,
                    Audience = p.Audience,
                    Amount = _calculators[_plays[p.Genre.ToString()].Type.ToString()].CalculatePrice(p),
                    Credits = _calculators[_plays[p.Genre.ToString()].Type.ToString()].CalculateCredits(p),
                    Genre = p.Genre.ToString()
                }).ToList()
            };
        }

        private void AssociatePlayIds(StatementRequest request)
        {
            foreach (var performance in request.Invoice.Performances)
            {
                var matchingPlay = request.Plays.Values.FirstOrDefault(play => play.Type == performance.Genre);

                if (matchingPlay != null)
                {
                    performance.UpdatePlayId(matchingPlay.PlayId);
                }
                else
                {
                    _logger.LogError($"Play not found for genre: {performance.Genre}");
                    throw new InvalidOperationException($"Play not found for genre: {performance.Genre}");
                }
            }
        }
    }
}