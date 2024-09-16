using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Application.ViewModels;

public class PerformanceViewModel
{
    [Required(ErrorMessage = "A propriedade 'PlayId' é obrigatória.")]
    public string PlayId { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Audience' é obrigatória.")]
    public int Audience { get; set; }

    public PerformanceEntity ToEntity()
    {
        return new PerformanceEntity(
            playId: PlayId,
            audience: Audience
        );
    }
}