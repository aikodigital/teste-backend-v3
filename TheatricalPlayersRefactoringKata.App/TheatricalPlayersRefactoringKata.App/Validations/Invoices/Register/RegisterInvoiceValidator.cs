
using FluentValidation;
using TheatricalPlayersRefactoringKata.Communication.Requests;
using TheatricalPlayersRefactoringKata.Exception;
using TheatricalPlayersRefactoringKata.Exception.ExceptionBase;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;

public class RegisterInvoiceValidator : AbstractValidator<RequestInvoice>
{
    public RegisterInvoiceValidator()
    {
        RuleFor(invoice => invoice.Customer).NotEmpty().WithMessage(ResourceErrorMessages.CUSTOMER_NAME_INVALID);
        RuleFor(invoice => invoice.Performances).NotEmpty().WithMessage(ResourceErrorMessages.PERFORMANCES_INVALID);
        RuleForEach(invoice => invoice.Performances)
                .ChildRules(performance =>
                {
                    performance.RuleFor(p => p.PlayId)
                       .NotEmpty().WithMessage(ResourceErrorMessages.PLAY_INVALID);
                        
                    performance.RuleFor(p => p.Audience)
                        .GreaterThan(0).WithMessage(ResourceErrorMessages.AUDIENCE_INVALID);
                });
    }
}
