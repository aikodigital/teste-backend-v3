using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.API.DTO;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Infrastructure.Services;
using TheatricalPlayersRefactoringKata.Interfaces.Repositories;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementPrinterController : Controller
    {
        private readonly IPlayRepository _playRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public StatementPrinterController(
            IPlayRepository playRepository,
            IInvoiceRepository invoiceRepository)
        {
            _playRepository = playRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] StatementDetailsDTO statementDetail)
        {
            var costumer = statementDetail.Customer;
            var invoice = await _invoiceRepository.GetByCustomerNameAsync(costumer);

            var playNames = statementDetail.PlayNames;
            var plays = new Dictionary<string, Play>();

            foreach (var playName in playNames)
            {
                var play = await _playRepository.GetByNameAsync(playName);
                plays.Add(playName, new Play(play.Name, play.Lines, play.Type));
            }

            StatementPrinter statementPrinter = new StatementPrinter();
            var output = statementPrinter.Print(invoice, plays, statementDetail.Format);

            return Ok(output);
        }
    }
}
