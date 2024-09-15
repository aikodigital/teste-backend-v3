using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TheatricalPlayersRefactoringKata.Application.Request;
using TheatricalPlayersRefactoringKata.Application.Response;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Interface;

namespace TheatricalPlayersRefactoringKata.Api.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageProducer _messageProducer;

        public InvoiceController(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IMessageProducer messageProducer)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _messageProducer = messageProducer;
        }

        /// <summary>
        /// Cadastro de um novo invoice
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Request exemplo:
        ///
        ///     POST /api/invoices
        ///     {
        ///        "customer": "BigCo",
        ///        "performances": [
        ///          {
        ///            "playId": "hamlet",
        ///            "audience": 55
        ///          },
        ///          {
        ///            "playId": "as-like",
        ///            "audience": 35
        ///          }
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Invoice cadastrado com sucesso</response>
        /// <response code="400">Erro no corpo da requisição</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PostInvoiceRequest request)
        {
            var validacao = request.Validar();
            if (!validacao.IsValid)
                return BadRequest(new RespostaPadrao(false, validacao.Errors[0].ErrorMessage));

            var performances = new List<Performance>();

            foreach (var perf in request.Performances)
                performances.Add(new Performance(perf.PlayId, perf.Audience));

            var invoice = new Invoice(request.Customer, performances);

            var invoiceId = await _invoiceRepository.Create(invoice);

            return Created($"/api/invoices/{invoiceId}", new RespostaPadrao(true, "Invoice cadastrado com sucesso"));
        }

        /// <summary>
        /// Buscar um invoice por id
        /// </summary>
        /// <remarks>
        /// Request exemplo:
        ///
        ///     GET /api/invoices/1
        ///
        /// </remarks>
        /// <response code="200">Invoice buscado com sucesso</response>
        /// <response code="404">Invoice não cadastrado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RespostaPadraoDados<BuscarInvoiceResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var invoice = await _invoiceRepository.Get(id);

            if (invoice == null)
                return NotFound(new RespostaPadrao(false, $"Invoice id {id} não cadastrado."));

            var performances = new List<BuscarInvoicePerformanceResponse>();
            foreach (var perf in invoice.Performances)
                performances.Add(new BuscarInvoicePerformanceResponse { Id = perf.Id, Audience = perf.Audience, PlayId = perf.PlayId });

            return Ok(new RespostaPadraoDados<BuscarInvoiceResponse>(true, "Invoice buscado com sucesso", new BuscarInvoiceResponse
            {
                Customer = invoice.Customer,
                Id = invoice.Id,
                Performances = performances
            }));
        }

        /// <summary>
        /// Buscar todos os invoices paginados
        /// </summary>
        /// <remarks>
        /// Request exemplo:
        ///
        ///     GET /api/invoices?take=10&amp;page=1
        ///
        /// </remarks>
        /// <response code="200">Invoices buscados com sucesso</response>
        /// <response code="400">Erro no corpo da requisição</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [ProducesResponseType(typeof(RespostaPadraoDados<List<BuscarInvoiceResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] int take = 10, [FromQuery] int page = 1)
        {
            if (take < 0 || take > 100)
                return BadRequest(new RespostaPadrao(false, "A Query 'take' deve estar entre 1 e 100"));

            if (page <= 0)
                return BadRequest(new RespostaPadrao(false, "A Query 'page' deve ser maior que 0"));

            var invoices = _invoiceRepository.GetAll(take, page);

            var invoicesResponse = new List<BuscarInvoiceResponse>();

            foreach (var invoice in invoices)
            {
                var performances = new List<BuscarInvoicePerformanceResponse>();
                foreach (var perf in invoice.Performances)
                    performances.Add(new BuscarInvoicePerformanceResponse { Id = perf.Id, Audience = perf.Audience, PlayId = perf.PlayId });

                invoicesResponse.Add(new BuscarInvoiceResponse
                {
                    Customer = invoice.Customer,
                    Id = invoice.Id,
                    Performances = performances
                });
            }

            return Ok(new RespostaPadraoDados<List<BuscarInvoiceResponse>>(true, "Invoices buscados com sucesso", invoicesResponse));
        }

        /// <summary>
        /// Deletar um invoice por id
        /// </summary>
        /// <remarks>
        /// Request exemplo:
        ///
        ///     DELETE /api/invoices/1
        ///
        /// </remarks>
        /// <response code="200">Invoice deletado com sucesso</response>
        /// <response code="404">Invoice não cadastrado</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var invoice = await _invoiceRepository.Get(id);

            if (invoice == null)
                return NotFound(new RespostaPadrao(false, $"Invoice id {id} não cadastrado."));

            _invoiceRepository.Delete(invoice);
            await _unitOfWork.Commit();

            return Ok(new RespostaPadrao(true, "Invoice deletado com sucesso"));
        }

        /// <summary>
        /// Imprimir um invoice por id
        /// </summary>
        /// <remarks>
        /// Request exemplo:
        ///
        ///     POST /api/invoices/1/imprimir?type=txt
        ///
        /// </remarks>
        /// <response code="202">Pedido de impressão aceito e em processamento</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(RespostaPadrao), StatusCodes.Status500InternalServerError)]
        [HttpPost("{id}/imprimir")]
        public async Task<IActionResult> Imprimir([FromRoute] int id, [FromQuery] string type)
        {
            var invoice = await _invoiceRepository.Get(id);

            if (invoice == null)
                return NotFound(new RespostaPadrao(false, $"Invoice id {id} não cadastrado."));

            var performancesWorkerRequest = invoice.Performances.Select(p => new PerformanceWorkerRequest { Id = p.Id, PlayId = p.PlayId, Audience = p.Audience }).ToList();

            var request = new PrinterWorkerRequest { Invoice = new InvoiceWorkerRequest { Customer = invoice.Customer, Performances = performancesWorkerRequest }, Type = type };

            _messageProducer.SendMessage(request);

            return Accepted(new RespostaPadrao(true, "O seu pedido de impressão foi recebido e será processado"));
        }
    }
}
