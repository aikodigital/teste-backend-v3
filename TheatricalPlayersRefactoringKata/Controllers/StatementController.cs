using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly StatementCalculator _statementCalculator;
        private readonly Dictionary<int, Play> _plays;

        public StatementController(StatementCalculator statementCalculator, Dictionary<int, Play> plays)
        {
            _statementCalculator = statementCalculator;
            _plays = plays;
        }

        [HttpGet("generate")]
        public ActionResult<string> GenerateStatement([FromQuery] Invoice invoice)
        {
            if (invoice == null || invoice.Performances == null || invoice.Performances.Count == 0)
            {
                return BadRequest("Invoice or performances cannot be null or empty.");
            }

            var result = _statementCalculator.Calculate(invoice);
            return Ok(result);
        }


    }
}
