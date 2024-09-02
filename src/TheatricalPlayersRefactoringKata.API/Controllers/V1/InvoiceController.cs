using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.API.Models;
using TheatricalPlayersRefactoringKata.API.Models.DTOs;
using TheatricalPlayersRefactoringKata.Application.Services.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.Exceptions;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories.Interfaces;

namespace TheatricalPlayersRefactoringKata.API.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{apiVersion:apiVersion}/invoice")]
public class InvoiceController : MainController
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceRepository invoiceRepository,
        IInvoiceService invoiceService)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceService = invoiceService;
    }

    /// <summary>
    /// Gets an invoice by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the invoice.</param>
    /// <returns>An ActionResult containing the invoice details.</returns>
    /// <response code="200">Returns the invoice with the specified ID.</response>
    /// <response code="404">If the invoice with the specified ID is not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomResponse<Invoice>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResponse<Invoice>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Invoice>> GetInvoiceById([FromRoute, Required] Guid id)
    {
        try
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);

            return Ok(new CustomResponse<Invoice>
            {
                Success = true,
                Data = invoice,
                Message = "Invoice retrieved successfully."
            });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new CustomResponse<Invoice>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Gets all invoices.
    /// </summary>
    /// <returns>A list of all invoices.</returns>
    /// <response code="200">Returns a list of all invoices.</response>
    [HttpGet]
    [ProducesResponseType(typeof(CustomResponse<IEnumerable<Invoice>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetAllInvoices()
    {
        var invoices = await _invoiceRepository.GetAllAsync();

        return Ok(new CustomResponse<IEnumerable<Invoice>>
        {
            Success = true,
            Data = invoices,
            Message = "Invoices retrieved successfully."
        });
    }

    /// <summary>
    /// Creates a new invoice.
    /// </summary>
    /// <param name="invoiceDto">The invoice data to be created.</param>
    /// <returns>An ActionResult indicating the result of the creation operation.</returns>
    /// <response code="201">Returns the created invoice.</response>
    /// <response code="400">If the input data is invalid.</response>
    /// <response code="500">If an unexpected error occurs during creation.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CustomResponse<Invoice>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateInvoice([FromBody, Required] InvoiceDto invoiceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "Invalid input data.",
                Errors = GetModelStateErrors()
            });
        }

        try
        {
            // Map the DTO to a domain object
            var invoice = MapToDomain(invoiceDto);

            // Create the invoice
            await _invoiceService.CreateAsync(invoice);

            return CreatedAtAction(nameof(GetInvoiceById),
                  new { apiVersion = GetApiVersion(), id = invoice.Id },
                new CustomResponse<Invoice>
                {
                    Success = true,
                    Data = invoice,
                    Message = "Invoice created successfully."
                });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new CustomResponse<Invoice>
            {
                Success = false,
                Message = e.Message,
            });
        }
    }

    private static Invoice MapToDomain(InvoiceDto invoiceDto)
    {
        var customer = new Customer(invoiceDto.Customer.Name);
        var performances = invoiceDto.Performances.Select(p => new Performance(
            new Play(p.Play.Title, Enum.Parse<GenreEnum>(p.Play.Genre, true), p.Play.Lines),
            p.Audience)).ToList();

        return new Invoice(customer, performances);
    }
}

