using TheatricalPlayersRefactoringKata.Domain.Dtos;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;

public interface IInvoiceService 
{
    Task<List<Invoice>> GetAll();
    Task<List<PlayDto>> GetAllPlays();
    Task<bool> Add(InvoiceDto invoiceDto);
}
