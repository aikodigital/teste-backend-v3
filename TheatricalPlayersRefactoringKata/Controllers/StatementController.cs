using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("statement")]
    public class StatementController : ControllerBase
    {
        [HttpPost("txt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTextBillingStatement([FromBody] Invoice invoice)
        {
            // coleção plays está mockada simulando uma base de dados.
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            try
            {
                var statement = new StatementPrinter();
                string txtStatement = statement.PrintText(invoice, plays);

                string dirPath = Path.Combine(Environment.CurrentDirectory, "statements", "txt");
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                await System.IO.File.WriteAllTextAsync(Path.Combine(dirPath, $"{DateTime.Now:ddMMyyHHmmss}_{invoice.Customer}_order.txt"), txtStatement);

                return Ok(txtStatement);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost("xml")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetXmlBillingStatement([FromBody] Invoice invoice)
        {
            // coleção plays está mockada simulando uma base de dados.
            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            try
            {
                var statement = new StatementPrinter();
                string xmlStatement = statement.PrintXml(invoice, plays);

                string dirPath = Path.Combine(Environment.CurrentDirectory, "statements", "xml");
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                await System.IO.File.WriteAllTextAsync(Path.Combine(dirPath, $"{DateTime.Now:ddMMyyHHmmss}_{invoice.Customer}_order.xml"), xmlStatement);

                return Ok(xmlStatement);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}