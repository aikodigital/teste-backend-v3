using System.ComponentModel.DataAnnotations;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.UI.WebAPI.ViewModels;

public class PlayViewModel
{
    [Required(ErrorMessage = "A propriedade 'Name' é obrigatória.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "A propriedade 'Lines' é obrigatória.")]
    public int Lines { get; set; }

    [Required(ErrorMessage = "A propriedade 'Type' é obrigatória.")]
    public string Type { get; set; } = null!;

    public PlayEntity ToEntity()
    {
        return new PlayEntity(
            name: Name,
            lines: Lines,
            type: Type
        );
    }
}