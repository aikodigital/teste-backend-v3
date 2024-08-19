using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata;

namespace TheaterInvoiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly StatementPrinter _statementPrinter;

        public InvoiceController(StatementPrinter statementPrinter)
        {
            _statementPrinter = statementPrinter;
        }

        [HttpPost("print")]
        public IActionResult PrintInvoice([FromBody] InvoiceRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Invalid request payload.");
            }

            if (string.IsNullOrEmpty(requestDto.Format) || !(requestDto.Format.Equals("xml", StringComparison.OrdinalIgnoreCase) || requestDto.Format.Equals("text", StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest("Invalid format. Supported formats are 'xml' and 'text'.");
            }

            var invoiceRequest = ConvertToInvoiceRequest(requestDto);

            var plays = GetPlays(invoiceRequest.Plays);

            var performances = invoiceRequest.Performances.Select(p => new TheatricalPlayersRefactoringKata.Performance(p.PlayId, p.Audience)).ToList();
            var invoice = new Invoice(invoiceRequest.Customer, performances);

            string result;
            try
            {
                result = invoiceRequest.Format.Equals("xml", StringComparison.OrdinalIgnoreCase) 
                    ? _statementPrinter.PrintXml(invoice, plays) 
                    : _statementPrinter.Print(invoice, plays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(result);
        }

        private InvoiceRequest ConvertToInvoiceRequest(InvoiceRequestDto dto)
        {
            var plays = dto.Plays.Values.ToList();
            return new InvoiceRequest(
                dto.Customer,
                dto.Performances,
                plays.ToDictionary(p => p.Name.ToLowerInvariant(), p => new Play(p.Name, p.Lines, p.Type)),
                dto.Format);
        }

       private Dictionary<string, Play> GetPlays(List<PlayDto> playDtos)
        {
            var plays = new Dictionary<string, Play>();
            foreach (var playDto in playDtos)
            {
                var playId = playDto.Name.ToLowerInvariant();
                plays[playId] = new Play(playDto.Name, playDto.Lines, playDto.Type);
            }
            return plays;
        }
    }
}

