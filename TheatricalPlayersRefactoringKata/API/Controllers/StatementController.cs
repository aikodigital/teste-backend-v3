using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Infrastructure;
using TheatricalPlayersRefactoringKata.API.Models;
using System.IO;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly IStatementGenerator _textStatementGenerator;
        private readonly IStatementGenerator _xmlStatementGenerator;
        private readonly IEnumerable<IPlayTypeCalculator> _calculators;

        public StatementController(
            IStatementGenerator textStatementGenerator,
            IEnumerable<IPlayTypeCalculator> calculators)
        {
            _textStatementGenerator = textStatementGenerator;
            _calculators = calculators;
            _xmlStatementGenerator = new XmlStatementGenerator(calculators);
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
        public ActionResult<string> GenerateXmlStatement([FromBody] StatementRequest request)
        {
            var statement = _xmlStatementGenerator.Generate(request.Invoice, request.Plays);

            var filePath = Path.Combine("C:\\Users\\User\\Documents\\teste-backend-v3\\TheatricalPlayersRefactoringKata\\arquivos\\xml", "statement.xml");

            System.IO.File.WriteAllText(filePath, statement);

            return Ok(statement);
        }
    }
}