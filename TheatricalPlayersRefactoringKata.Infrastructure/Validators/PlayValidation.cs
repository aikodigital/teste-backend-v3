using FluentValidation;
using TheatricalPlayersRefactoringKata.Application.DTOs.PlayDTOs;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Validators
{
    public class PlayValidation : AbstractValidator<PlayRequest>
    {
        public PlayValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Play Name can not be empty!")
                .NotNull()
                .WithMessage("Play Name can not be null!")
                .MinimumLength(2)
                .WithMessage("Play Name must contain at least 2 characters!")
                .MaximumLength(100)
                .WithMessage("Play Name can not exceed 100 characters!");

            RuleFor(p => p.Lines)
                .NotEmpty()
                .WithMessage("Lines cannot be empty")
                .NotNull()
                .WithMessage("Lines cannot be null")
                .GreaterThan(0)
                .WithMessage("The number of lines must be greater than 0");

            RuleFor(p => p.Genre)
                .NotNull()
                .WithMessage("Genre cannot be null")
                .IsInEnum()
                .WithMessage("Invalid Genre specified. Please select a valid Genre");

        }
    }
}
