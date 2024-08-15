
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
        RuleFor(invoice => invoice.Performances).NotEmpty().WithMessage(ResourceErrorMessages.PLAY_TYPE_INVALID);
    }
}
