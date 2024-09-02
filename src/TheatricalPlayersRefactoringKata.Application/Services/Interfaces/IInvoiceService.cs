using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Application.Services.Interfaces;

public interface IInvoiceService
{
    Task CreateAsync(Invoice invoice);
}