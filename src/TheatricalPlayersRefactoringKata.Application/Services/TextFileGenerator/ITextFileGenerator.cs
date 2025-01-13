using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator
{
    public interface ITextFileGenerator
    {
        public string TextFile(Invoice invoice, List<Statement> statements);
    }
}
