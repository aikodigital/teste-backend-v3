using aikodigital_teste_backend_v3.Dto;
using Microsoft.AspNetCore.Mvc;

namespace aikodigital_teste_backend_v3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementController : ControllerBase
    {
        private readonly StatementPrinterService _statementPrinterService;

        public StatementController(StatementPrinterService statementPrinterService)
        {
            _statementPrinterService = statementPrinterService;
        }

        [HttpPost("print")]
        public async Task<IActionResult> PrintStatement([FromBody] StatementPrintDto statementPrintDto)
        {
            if (statementPrintDto == null)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                await _statementPrinterService.Print(statementPrintDto);
                return Ok("Statement printed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
