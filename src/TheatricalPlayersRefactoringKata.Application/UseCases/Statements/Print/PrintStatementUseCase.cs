using TheatricalPlayersRefactoringKata.Application.Services.StatementManipulate;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.ValueObjects;
using TheatricalPlayersRefactoringKata.Exception.ExceptionsBase;
using TheatricalPlayersRefactoringKata.Infraestructure.Services.FileGenerator;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Statements.Print
{
    public class PrintStatementUseCase : IPrintStatementUseCase
    {
        private readonly IStatementPrinterService _statementPrinterService;
        private readonly IFileGenerator _fileGenerator;
        
        public PrintStatementUseCase(IStatementPrinterService statementPrinterService, IFileGenerator FileGenerator)
        {
            _statementPrinterService = statementPrinterService;
            _fileGenerator = FileGenerator;
        }

        public async Task<ResponsePrintStatementJson> ExecuteAsync(RequestPrintStatementJson request)
        {
            // validates the datas of request
            Validate(request);

            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
            plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
            plays.Add("othello", new Play("Othello", 3560, "tragedy"));
            plays.Add("henry-v", new Play("Henry V", 3227, "history"));
            plays.Add("john", new Play("King John", 2648, "history"));
            plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

            Invoice invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
                }
            );

            // gets the text of the file
            string formatFile = request.FormatFile;
            
            var textFile = await _statementPrinterService.PrintAsync(invoice, plays, formatFile);

            // generates the file
            await _fileGenerator.FileGeneratorAsync(textFile, formatFile);

            return new ResponsePrintStatementJson() { TextFile = textFile };
        }

        private void Validate(RequestPrintStatementJson request)
        {
            var validator = new StatementValidator();
            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
