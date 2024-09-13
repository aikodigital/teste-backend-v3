using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.UI.WebAPI.ViewModels;

public class InvoiceViewModel
{
    [Required(ErrorMessage = "A propriedade 'Customer' é obrigatória.")]
    public string Customer { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Performances' é obrigatória.")]
    public List<PerformanceViewModel> Performances { get; set; } = null!;

    public InvoiceEntity ToEntity()
    {
        return new InvoiceEntity(
            customer: Customer,
            performances: Performances.Select(x => x.ToEntity()).ToList()
        );
    }
}