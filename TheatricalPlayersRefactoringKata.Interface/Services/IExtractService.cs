using TheatricalPlayersRefactoringKata.Domain.Model.Entity;
using TheatricalPlayersRefactoringKata.Domain.Model.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Interface.Services
{
    public interface IExtractService
    {
        string GenerateExtract(Invoice invoice);
        string GenerateExtract(Invoice invoice, ExtractTypeEnum extractType);
    }
}