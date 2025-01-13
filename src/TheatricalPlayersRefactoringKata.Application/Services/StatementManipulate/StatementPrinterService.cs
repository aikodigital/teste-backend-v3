using TheatricalPlayersRefactoringKata.Application.Services.TextFileGenerator;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Services.StatementManipulate
{
    public class StatementPrinterService
    {
        public async Task<string> PrintAsync(Invoice invoice, Dictionary<string, Play> plays, string formatFile)
        {
            // gets the data of stataments
            var statements = StatementDataService.StatementsData(invoice, plays);

            // gets the file text with the stataments
            var result = await TextFileGeneratorService.TextFileAsync(invoice, statements, formatFile);

            return result;
        }
    }
}
