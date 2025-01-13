using FluentValidation; //library that validates the data
using TheatricalPlayersRefactoringKata.Application.Services.Mapping;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Statements
{
    public class StatementValidator : AbstractValidator<RequestPrintStatementJson>
    {
        public StatementValidator()
        {
            RuleFor(statement => statement.FormatFile).Must(formatFile => MappingService.FormatFileMapping.ContainsKey(formatFile)).WithMessage(ResourceErrorMessages.INVALID_FORMAT_FILE);
        }
    }
}
