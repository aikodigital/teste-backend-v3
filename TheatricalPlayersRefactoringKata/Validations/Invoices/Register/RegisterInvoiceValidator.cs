
using FluentValidation;
using System;
using TheatricalPlayersRefactoringKata.Communication.Requests;

namespace TheatricalPlayersRefactoringKata.Validations.Invoices.Register;

public class RegisterInvoiceValidator : AbstractValidator<RequestInvoice>
{
    public RegisterInvoiceValidator()
    {
        RuleFor(invoice => invoice.Title).NotEmpty().WithMessage(ResourceErrorMessages.Title_Required);
        RuleFor(invoice => invoice.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.Amount_Greather_Than_0);
        RuleFor(invoice => invoice.Date).LessThan(DateTime.UtcNow).WithMessage(ResourceErrorMessages.Expenses_Not_In_Future);
        RuleFor(invoice => invoice).IsInEnum().WithMessage(ResourceErrorMessages.Payment_Invalid);
    }
}
