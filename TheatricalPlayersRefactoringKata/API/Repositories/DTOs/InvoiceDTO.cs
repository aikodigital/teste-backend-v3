namespace TheatricalPlayersRefactoringKata.API.Repositories.DTOs;

public record InvoiceRequest(
    string CustomerName,
    List<Guid>? PerformancesIds
);


public enum ReceiptType
{
    Text,
    Xml
}