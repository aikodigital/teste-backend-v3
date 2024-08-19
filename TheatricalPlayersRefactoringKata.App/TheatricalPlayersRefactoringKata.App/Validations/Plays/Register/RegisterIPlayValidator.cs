
using FluentValidation;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Exception;

namespace TheatricalPlayersRefactoringKata.App.Validations.Plays.Register;

public class RegisterPlayValidator : AbstractValidator<RequestPlay>
{
    public RegisterPlayValidator()
    {
        RuleFor(play => play.Name).NotEmpty().WithMessage(ResourceErrorMessages.CUSTOMER_NAME_INVALID);
        RuleFor(play => play.Lines).LessThan(4000).GreaterThan(1000).WithMessage(ResourceErrorMessages.LINES_IN_INTERVAL);
        RuleFor(play => play.Type).IsInEnum().WithMessage(ResourceErrorMessages.PLAY_TYPE_INVALID);
    }
}
