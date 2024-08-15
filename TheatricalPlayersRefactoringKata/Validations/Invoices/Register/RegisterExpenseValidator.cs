using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception;
using FluentValidation;

namespace TheatricalPlayersRefactoringKata.Validations.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestExpenses>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.Title_Required);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.Amount_Greather_Than_0);
        RuleFor(expense => expense.Date).LessThan(DateTime.UtcNow).WithMessage(ResourceErrorMessages.Expenses_Not_In_Future);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.Payment_Invalid);
    }
}
