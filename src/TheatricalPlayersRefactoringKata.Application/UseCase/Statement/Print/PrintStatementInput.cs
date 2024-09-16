using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Application.Models;
using TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;
using TheatricalPlayersRefactoringKata.Application.ViewModels;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Print;

public class PrintStatementInput
{
    [Required(ErrorMessage = "A propriedade 'Plays' é obrigatória.")]
    public Dictionary<string, PlayViewModel> Plays { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Invoice' é obrigatória.")]
    public InvoiceViewModel Invoice { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'PrintFormat' é obrigatória.")]
    public PrintFormatEnum PrintFormat { get; set; }

    public CreateStatementInput ToCreateStatementInput()
    {
        return new CreateStatementInput
        {
            Plays = Plays,
            Invoice = Invoice,
        };
    }
}
