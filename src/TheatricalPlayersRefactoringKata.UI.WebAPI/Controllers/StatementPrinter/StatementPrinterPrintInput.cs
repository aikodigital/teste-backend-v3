using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.UI.WebAPI.ViewModels;

namespace TheatricalPlayersRefactoringKata.UI.WebAPI.Controllers.StatementPrinter;

public class StatementPrinterPrintInput
{
    [Required(ErrorMessage = "A propriedade 'Plays' é obrigatória.")]
    public Dictionary<string, PlayViewModel> Plays { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Invoice' é obrigatória.")]
    public InvoiceViewModel Invoice { get; set; } = null!;
}
