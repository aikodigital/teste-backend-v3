using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Application.ViewModels;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;

public class CreateStatementInput
{
    [Required(ErrorMessage = "A propriedade 'Plays' é obrigatória.")]
    public Dictionary<string, PlayViewModel> Plays { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Invoice' é obrigatória.")]
    public InvoiceViewModel Invoice { get; set; } = null!;
}
