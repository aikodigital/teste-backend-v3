using FluentValidation;
using TheatricalPlayersRefactoringKata.Application.DTOs.InvoiceDTOs;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Validators
{
    public class InvoiceValidation : AbstractValidator<InvoiceRequest>
    {
        public InvoiceValidation()
        {
            RuleFor(i => i.Customer)
                .NotEmpty()
                .WithMessage("Customer name cannot be empty")
                .NotNull()
                .WithMessage("Customer name cannot be null")
                .MinimumLength(2)
                .WithMessage("Customer name must contain at least 2 characters")
                .MaximumLength(100)
                .WithMessage("Customer name cannot exceed 100 characters");

            RuleFor(i => i.PerformanceIds)
                .NotNull()
                .WithMessage("PerformanceIds cannot be null")
                .NotEmpty()
                .WithMessage("At least one performance must be selected")
                .Must(ids => ids.All(id => id != Guid.Empty))
                .WithMessage("Each performance ID must be a valid, non-empty GUID");
        }
    }
}
