using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Performance;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;
using TheatricalPlayersRefactoringKata.Server.DTOs.Play;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly PlayRepository _repository;
        private readonly IMapper _mapper;

        public InvoiceController(PlayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Simula as performances e retorna o extrato da fatura no formato especificado.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SimulateInvoiceResponse))]
        public async Task<IActionResult> Simulate([FromBody] SimulateInvoiceRequest request)
        {
            AbstractOutputWritter? outputWritter = AbstractOutputWritter.FromString(request.Invoice.OutputWritterType);
            if (outputWritter == null)
            {
                return BadRequest("Invalid output writter type");
            }

            Dictionary<string, Play> plays = new Dictionary<string, Play>();

            // Verify whether all plays in the invoice exist
            foreach (PerformanceDTO performance in request.Invoice.Performances)
            {
                PlayEntity? play = await _repository.GetByTitle(performance.PlayId);
                if (play == null)
                {
                    return BadRequest($"Play {performance.PlayId} not found");
                }

                plays.Add(play.Name, _mapper.Map<Play>(play));
            }

            // Map plays to domain objects
            Invoice invoice = new Invoice(request.Invoice.Customer, _mapper.Map<List<Performance>>(request.Invoice.Performances));

            // Simulate the invoice and return the statement
            string? statementResult = new StatementPrinter().Print(invoice, plays, outputWritter, null);
            if (statementResult == null)
            {
                return BadRequest("Failed to generate statement");
            }

            return Ok(new SimulateInvoiceResponse { Statement = statementResult });
        }
    }
}