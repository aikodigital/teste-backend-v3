using FluentValidation;
using FluentValidation.Results;

namespace TheatricalPlayersRefactoringKata.Application.Request
{
    public class PostInvoiceRequest
    {
        public string Customer { get; set; }
        public List<PostPerformanceRequest> Performances { get; set; }
        public ValidationResult Validar()
        {
            return new PostInvoiceRequestValidator().Validate(this);
        }
    }

    public class PostPerformanceRequest
    {
        public string PlayId { get; set; }
        public int Audience { get; set; }
    }

    public class PostInvoiceRequestValidator : AbstractValidator<PostInvoiceRequest>
    {
        public PostInvoiceRequestValidator()
        {
            RuleFor(x => x.Customer)
                .NotEmpty()
                .WithMessage("O Campo Customer é obrigatório")
                .MaximumLength(100)
                .WithMessage("O Campo Customer deve ter no máximo 100 caracteres");

            RuleFor(x => x.Performances)
                .NotEmpty()
                .WithMessage("O Campo Performances é obrigatório e deve possuir pelo menos 1 elemento");

            RuleForEach(x => x.Performances).SetValidator(new PostPerformanceRequestValidator());
        }
    }

    public class PostPerformanceRequestValidator : AbstractValidator<PostPerformanceRequest>
    {
        public PostPerformanceRequestValidator()
        {
            RuleFor(x => x.PlayId)
                .NotEmpty().WithMessage("O campo PlayId é obrigatório.")
                .MaximumLength(100)
                .WithMessage("O Campo PlayId deve ter no máximo 100 caracteres");

            RuleFor(x => x.Audience)
                .GreaterThan(0).WithMessage("O campo Audience deve ser maior que 0.");
        }
    }
}
