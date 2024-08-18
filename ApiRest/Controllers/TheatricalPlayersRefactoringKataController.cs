using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.DTOs;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheatricalPlayersRefactoringKataController : ControllerBase
    {
        private readonly ApiDbContext _db;
        private readonly PlayCalculatorFactory _calculatorFactory;

        public TheatricalPlayersRefactoringKataController(ApiDbContext db, PlayCalculatorFactory calculatorFactory)
        {
            _db = db;
            _calculatorFactory = calculatorFactory;
        }

        [HttpPost]
        [Route("generate-statement")]
        public IActionResult GenerateStatement([FromBody] StatementRequestDto requestDto)
        {
            if (requestDto == null || string.IsNullOrEmpty(requestDto.Customer) || string.IsNullOrEmpty(requestDto.PlayName))
            {
                return BadRequest("Invalid request data.");
            }

            // Encontra a fatura pelo cliente
            var invoice = _db.Invoices
                .Include(i => i.Performances)
                .ThenInclude(p => p.Play)
                .FirstOrDefault(i => i.Customer == requestDto.Customer);

            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            // Encontrar o Play correspondente ao nome fornecido
            var play = _db.Plays.FirstOrDefault(p => p.Name == requestDto.PlayName);
            if (play == null)
            {
                return NotFound("Play not found.");
            }

            var statement = new Statement
            {
                Customer = requestDto.Customer,
                TotalAmount = 0,
                TotalCredits = 0
            };

            // Processar somente as performances que correspondem ao Play
            foreach (var performance in invoice.Performances)
            {
                if (performance.PlayId == play.Id)  // Certifique-se de que está usando o ID correto
                {
                    var calculator = _calculatorFactory.GetCalculator(play.Type);
                    var amount = calculator.CalculateAmount(play, performance);
                    var credits = calculator.CalculateVolumeCredits(play, performance);

                    var performanceSummary = new PerformanceSummary
                    {
                        PlayName = play.Name,  // Use o nome do Play aqui
                        Amount = amount,
                        Audience = performance.Audience
                    };

                    statement.Performances.Add(performanceSummary);
                    statement.TotalAmount += amount;
                    statement.TotalCredits += credits;
                }
            }

            if (!statement.Performances.Any())
            {
                return NotFound("No performances found for the specified play.");
            }

            return Ok(new StatementDto
            {
                Customer = statement.Customer,
                Performances = statement.Performances.Select(p => new PerformanceSummaryDto
                {
                    PlayName = p.PlayName,
                    Amount = p.Amount,
                    Audience = p.Audience
                }).ToList(),
                TotalAmount = statement.TotalAmount,
                TotalCredits = statement.TotalCredits
            });
        }


        public class StatementRequestDto
        {
            public string Customer { get; set; }
            public string PlayName { get; set; }
        }

    }
}

