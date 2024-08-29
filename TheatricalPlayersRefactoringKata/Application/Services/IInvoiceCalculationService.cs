using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public interface IInvoiceCalculationService
{
    Task<decimal> CalculateTotalValue(Invoice invoice);
}
