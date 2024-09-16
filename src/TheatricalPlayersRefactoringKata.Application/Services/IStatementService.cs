using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services;

public interface IStatementService
{
    StatementEntity Create(InvoiceEntity invoice, Dictionary<string, PlayEntity> plays);
    
    string PrintText(StatementEntity statement);
    
    string PrintXml(StatementEntity statement);
}