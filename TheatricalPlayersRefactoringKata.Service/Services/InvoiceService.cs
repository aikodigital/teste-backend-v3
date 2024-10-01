using TheatricalPlayersRefactoringKata.Domain.Dtos;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;

namespace TheatricalPlayersRefactoringKata.Service.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _iinvoiceRepository; 
    private readonly IPlayRepository _iplayRepository; 
    private readonly IStatementPrinterService _iStatementPrinterService; 

    public InvoiceService(IInvoiceRepository iinvoiceRepository, IPlayRepository iplayRepository, IStatementPrinterService iStatementPrinterService)
    {
        _iinvoiceRepository = iinvoiceRepository;
        _iplayRepository = iplayRepository;
        _iStatementPrinterService = iStatementPrinterService;
    }

    public async Task<List<Invoice>> GetAll() => await _iinvoiceRepository.GetAll();

    public async Task<List<PlayDto>> GetAllPlays() 
    { 
        List<Play> playsDb = await _iplayRepository.GetAll();

        List<PlayDto> playsDto = new();

        foreach (var play in playsDb) 
        {
            playsDto.Add(new PlayDto
            {
                Id = play.Id,
                Lines = play.Lines,
                Name = play.Name,
                TypeGenreId = play.TypeGenreId,
            });
        }

        return playsDto;
    }

    public async Task<bool> Add(InvoiceDto invoiceDto) 
    {
        Invoice invoiceDb = new()
        {
            Customer = invoiceDto.Customer
        };

        foreach (var performance in invoiceDto.Performances)
        {
            var playDb = await _iplayRepository.GetById(performance.PlayId);

            invoiceDb.Performances.Add(new Performance
            {
                Audience = performance.Audience,
                Play = playDb,
                PlayId = performance.PlayId
            });
        }

        await _iinvoiceRepository.AddAsync(invoiceDb);
        await _iStatementPrinterService.AddStatementCustomer(invoiceDb);

        return invoiceDb.Id > 0;
    }
}