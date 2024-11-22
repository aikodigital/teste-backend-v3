using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Api.Model;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {

        private readonly IStatementPrinter _statementPrinter;
        private readonly IStatementFile _statementFile;
        private readonly IConfiguration _configuration;

        public TestController(IStatementPrinter statementPrinter, IConfiguration configuration, IStatementFile statementFile)
        {
            _statementPrinter = statementPrinter;
            _configuration = configuration;
            _statementFile = statementFile;
        }

        /// <summary>
        /// Prints the statement to the requested format.
        /// </summary>
        /// <param name="format">The format of the statement (e.g., 'text', 'xml').</param>
        /// <returns>The printed statement in the specified format.</returns>
        /// <response code="200">Returns the printed statement.</response>
        /// <response code="400">Bad request if format is invalid.</response>
        [HttpPost("Print-Statement")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PrintStatement(string format)
        {
            try
            {
                var Invoice = MockData.GetInvoice1();
                var Plays = MockData.GetPlays1();

                var result = _statementPrinter.Print(Invoice, Plays, format);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        /// <summary>
        /// Generates a statement file in the requested format and saves it.
        /// </summary>
        /// <param name="format">The format of the generated file (e.g., 'text', 'xml').</param>
        /// <returns>A message indicating the file was successfully generated.</returns>
        /// <response code="200">File successfully generated.</response>
        /// <response code="500">Internal server error if an unexpected error occurs.</response>
        [HttpPost("Generate-File")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateStatementFile(string format)
        {
            try
            {
                var Invoice = MockData.GetInvoice1();
                var Plays = MockData.GetPlays1();

                var result = _statementPrinter.Print(Invoice, Plays, format);
                
                var outputDirectory = _configuration["FileSettings:OutputDirectory"];
                var fileName = $"Statement_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.{format}";

                var filePath = Path.Combine(outputDirectory, fileName);

                await _statementFile.SaveFileAsync(outputDirectory, fileName, result);

                return Ok(new { Message = $"Arquivo {format} gerado com sucesso no caminho: {filePath}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
