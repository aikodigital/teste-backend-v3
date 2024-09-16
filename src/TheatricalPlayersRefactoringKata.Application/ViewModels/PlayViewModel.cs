using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Application.ViewModels;

public class PlayViewModel
{
    [Required(ErrorMessage = "A propriedade 'Name' é obrigatória.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Lines' é obrigatória.")]
    public int Lines { get; set; }

    [Required(ErrorMessage = "A propriedade 'Type' é obrigatória.")]
    public PlayTypeEnum Type { get; set; }

    public PlayEntity ToEntity()
    {
        return new PlayEntity(
            name: Name,
            lines: Lines,
            type: Type
        );
    }
}