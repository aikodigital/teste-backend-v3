using TheatricalPlayersRefactoringKata.API.Repositories.DTOs;

namespace TheatricalPlayersRefactoringKata.API.Repositories.Validators;

public static class InvoiceValidator
{
    public static bool IsValid(InvoiceRequest? invoiceRequest)
    {
        return invoiceRequest == null && !string.IsNullOrEmpty(invoiceRequest?.CustomerName);
    }
}