namespace TheatricalPlayersRefactoringKata.API.Repositories.DTOs;

public abstract record InvoiceRequest(
    string CustomerName,
    List<Guid>? PerformancesIds
);


public enum ReceiptType
{
    Text,
    Xml
}