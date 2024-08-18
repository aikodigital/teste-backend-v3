using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.DTOs;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using TheatricalPlayersRefactoringKata.API.Repositories.Interfaces;
namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheaterPrinterController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPlayRepository _playRepository;

        public TheaterPrinterController(IInvoiceRepository invoiceRepository, IPlayRepository playRepository)
        {
            _invoiceRepository = invoiceRepository;
            _playRepository = playRepository;
        }

        [HttpPost]
        [Route("print")]
        public async Task<IActionResult> PrintStatement([FromBody] PrintRequestDto printRequestDto)
        {

            var costumer = printRequestDto.Customer;
            var playNames = printRequestDto.PlayNames;

            var invoice = await _invoiceRepository.GetByCustomerAsync(costumer);

            var plays = new Dictionary<string, Play>();
            foreach (var play in playNames)
            {
                var playObejct = await _playRepository.GetByNameAsync(play);
                plays.Add(play, new Play(playObejct.Name, playObejct.Lines, playObejct.Type));
            }
            StatementPrinter statementPrinter = new StatementPrinter();
            var result2 = statementPrinter.Print(invoice, plays, printRequestDto.Format);
            return Ok(result2);
        }
    }
}