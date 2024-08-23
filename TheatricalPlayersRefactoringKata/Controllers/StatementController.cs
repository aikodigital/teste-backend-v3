using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Data;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("statements")]
    public class StatementController : ControllerBase
    {
        [HttpPost("txt")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTextBillingStatement([FromServices] AppDbContext db, [FromBody] Invoice invoice)
        {
            try
            {
                var statement = new StatementPrinter(db);
                string txtStatement = statement.PrintText(invoice);

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
        public async Task<IActionResult> GetXmlBillingStatement([FromServices] AppDbContext db, [FromBody] Invoice invoice)
        {
            try
            {
                var statement = new StatementPrinter(db);
                string xmlStatement = statement.PrintXml(invoice);

                string dirPath = Path.Combine(Environment.CurrentDirectory, "statements", "xml");
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                await System.IO.File.WriteAllTextAsync(Path.Combine(dirPath, $"{DateTime.Now:ddMMyyHHmmss}_{invoice.Customer}_order.xml"), xmlStatement);

                return Ok(xmlStatement);
            }
            catch (ArgumentOutOfRangeException ex) { return NotFound(ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBillingStatement([FromServices] AppDbContext db)
        {
            try
            {
                var printLog = await db.StatementLogs
                                       .GroupBy(log => log.Costumer)
                                       .Select(statement => new
                                       {
                                           Costumer = statement.Key,
                                           AmountOwed = (decimal)statement.Sum(plays => (float)plays.Amount),
                                           OwedCredits = statement.Sum(plays => (int)plays.Credits)
                                       }).ToArrayAsync();

                return printLog.Any() ? Ok(printLog) : NoContent();
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("costumer/{costumerName}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBillingStatementByCostumerName([FromServices] AppDbContext db, string costumerName)
        {
            try
            {
                var printLog = await db.StatementLogs
                                       .Where(statement => statement.Costumer.ToLower() == costumerName.ToLower())
                                       .ToArrayAsync();

                return printLog.Any() ? Ok(printLog) : NoContent();
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}