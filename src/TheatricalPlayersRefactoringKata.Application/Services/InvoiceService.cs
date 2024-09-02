using Microsoft.Extensions.Logging;
using TheatricalPlayersRefactoringKata.Application.Factories;
using TheatricalPlayersRefactoringKata.Application.Services.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;
using TheatricalPlayersRefactoringKata.Infrastructure.Data;
using TheatricalPlayersRefactoringKata.Infrastructure.Repositories.Interfaces;

namespace TheatricalPlayersRefactoringKata.Application.Services;

/// <summary>
/// Service for handling invoice-related operations.
/// </summary>
public class InvoiceService : IInvoiceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly StatementProducer _statementProducer;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(IUnitOfWork unitOfWork, 
        IInvoiceRepository invoiceRepository,
        StatementProducer statementProducer,
        ILogger<InvoiceService> logger)
    {
        _unitOfWork = unitOfWork;
        _invoiceRepository = invoiceRepository;
        _logger = logger;
        _statementProducer = statementProducer;
    }

    /// <summary>
    /// Creates a new invoice asynchronously.
    /// </summary>
    /// <param name="invoice">The invoice to create.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when the invoice cannot be saved to the database.</exception>
    public async Task CreateAsync(Invoice invoice)
    {
        // Calculate the amount and credits for each performance in the invoice
        foreach (var performance in invoice.Performances)
        {
            var calculator = PerformanceFactory.CreateStrategy(performance.Play.Genre);
            performance.Amount = calculator.CalculateAmount(performance);
            performance.Credits = calculator.CalculateCredits(performance);
        }

        // Add the invoice to the repository and save it to the database
        await _invoiceRepository.AddAsync(invoice);
        var result = await _unitOfWork.CommitAsync();

        if (!result)
        {
            _logger.LogError("Failed to save invoice to the database.");
            throw new Exception("Failed to save invoice to the database.");
        }

        // Send the invoice to the queue
        _statementProducer.SendToQueue(invoice);
    }
}