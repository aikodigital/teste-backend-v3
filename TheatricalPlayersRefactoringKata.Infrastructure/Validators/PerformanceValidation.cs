using FluentValidation;
using TheatricalPlayersRefactoringKata.Application.DTOs.PerformanceDTOs;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Validators
{
    public class PerformanceValidation : AbstractValidator<PerformanceRequest>
    {
        public PerformanceValidation()
        {
            RuleFor(p => p.PlayId)
                .NotEmpty()
                .WithMessage("PlayId cannot be empty")
                .NotEqual(Guid.Empty)
                .WithMessage("PlayId must be a valid GUID");

            RuleFor(p => p.Audience)
                .GreaterThan(0)
                .WithMessage("Audience must be greater than 0");
        }
    }
}