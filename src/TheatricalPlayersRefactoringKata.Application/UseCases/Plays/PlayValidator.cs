using FluentValidation;
using TheatricalPlayersRefactoringKata.Application.Services.Mapping;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersRefactoringKata.Application.UseCases.Plays
{
    public class PlayValidator : AbstractValidator<RequestPlayJson>
    {
        public PlayValidator()
        {
            RuleFor(play => play.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_NOT_EMPTY);
            RuleFor(play => play.Lines).GreaterThan(0).WithMessage(ResourceErrorMessages.LINES_MUST_BE_GREATER_THAN_ZERO);
            RuleFor(play => play.Type).Must(genre => MappingService.GenreMapping.ContainsKey(genre)).WithMessage(ResourceErrorMessages.INVALID_FORMAT_GENRE);
        }
    }
}
