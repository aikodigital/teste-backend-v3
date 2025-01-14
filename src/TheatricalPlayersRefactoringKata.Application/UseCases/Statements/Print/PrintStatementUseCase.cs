using TheatricalPlayersRefactoringKata.Application.Services.StatementManipulate;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Communication.Responses;
using TheatricalPlayersRefactoringKata.Exception;
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

            // gets values of request
            var plays = request.Plays;
            var invoice = request.Invoice;
            var formatFile = request.FormatFile;
            
            var textFile = await _statementPrinterService.PrintAsync(invoice, plays, formatFile);

            // generates the file
            var result = await _fileGenerator.FileGeneratorAsync(textFile, formatFile);

            if (!result)
            {
                throw new ErrorOnValidationException(new List<string>() { ResourceErrorMessages.FILE_GENERATOR_ERROR });
            }

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
