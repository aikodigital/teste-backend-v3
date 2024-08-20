using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Invoice;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.InvoiceHistory;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Performance;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.InvoiceHistory;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.PerformanceHistory;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;
using TheatricalPlayersRefactoringKata.Server.DTOs.InvoiceHistory;
using TheatricalPlayersRefactoringKata.Server.DTOs.Play;
using TheatricalPlayersRefactoringKata.Server.Mappers.Extensions;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly PlayRepository _playRepository;
        private readonly InvoiceHistoryRepository _invoiceHistoryRepository;
        private readonly PerformanceHistoryRepository _performanceHistoryRepository;
        private readonly IMapper _mapper;

        public InvoiceController(PlayRepository playRepository, InvoiceHistoryRepository invoiceRepository, PerformanceHistoryRepository performanceHistory, IMapper mapper)
        {
            _playRepository = playRepository;
            _invoiceHistoryRepository = invoiceRepository;
            _performanceHistoryRepository = performanceHistory;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna o histórico de invoice dado um respectivo id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("history/byId/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetHistoryByIdResponse))]
        public async Task<IActionResult> GetHistoryById(int id)
        {
            InvoiceHistoryEntity? invoiceHistory = await _invoiceHistoryRepository.GetById(id);
            if (invoiceHistory == null)
            {
                return NotFound();
            }

            return Ok(new GetHistoryByIdResponse { InvoiceHistory = _mapper.Map<InvoiceHistoryDTO>(invoiceHistory) });
        }

        /// <summary>
        /// Retorna o histórico de invoice(s) de um cliente. 
        /// </summary>
        /// <param name="customer"></param>
        [HttpGet("history/byCustomer/{customer}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InvoiceHistoryDTO>))]
        public async Task<IActionResult> GetHistoryByCustomer(string customer)
        {
            IEnumerable<InvoiceHistoryEntity>? invoicesHistory = await _invoiceHistoryRepository.GetByCustomer(customer);
            if (invoicesHistory == null)
            {
                return Ok(new GetHistoryByCustomerResponse { InvoicesHistory = new List<InvoiceHistoryDTO>() });
            }

            return Ok(new GetHistoryByCustomerResponse { InvoicesHistory = invoicesHistory.Select(invoice => _mapper.Map<InvoiceHistoryDTO>(invoice)).ToList() });
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
                PlayEntity? play = await _playRepository.GetByTitle(performance.PlayId);
                if (play == null)
                {
                    return BadRequest($"Play {performance.PlayId} not found");
                }

                // In the case of repeated plays, only the first one is considered
                if (!plays.ContainsKey(play.Name))
                {
                    plays.Add(play.Name, _mapper.Map<Play>(play));
                }
            }

            // Map plays to domain objects
            Invoice invoice = new Invoice(request.Invoice.Customer, _mapper.Map<List<Performance>>(request.Invoice.Performances));

            // Simulate the invoice and return the statement
            string? statementResult = new StatementPrinter().Print(invoice, plays, outputWritter, null);
            if (statementResult == null)
            {
                return BadRequest("Failed to generate statement");
            }

            try
            {
                // Register the invoice history
                InvoiceHistoryEntity invoiceHistoryEntityEntry = await _invoiceHistoryRepository.Insert(_mapper.MapWithPostProcessing<Invoice, InvoiceHistoryEntity>(invoice, destination =>
                {
                    destination.DateOfInvoice = DateTime.Now.ToString("yyyy-MM-dd");
                }));

                // Iterate over performances and register them in the history
                foreach (Performance performance in invoice.Performances)
                {
                    await _performanceHistoryRepository.Insert(_mapper.MapWithPostProcessing<Performance, PerformanceHistoryEntity>(performance, destination =>
                    {
                        destination.InvoiceHistoryId = invoiceHistoryEntityEntry.Id;
                    }));
                }
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine($"Failed to register invoice and performances history with error: {exception.Message}");
            }

            return Ok(new SimulateInvoiceResponse { Statement = statementResult });
        }
    }
}