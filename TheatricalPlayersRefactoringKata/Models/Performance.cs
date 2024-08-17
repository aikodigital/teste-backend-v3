using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string PlayId { get; set; }

    [ForeignKey(nameof(PlayId))]
    public Play Play { get; set; }

    public int Audience { get; set; }

    [Required]
    public int InvoiceId { get; set; }

    [ForeignKey(nameof(InvoiceId))]
    public Invoice Invoice { get; set; }

    public Performance()
    {
    }

    public Performance(string playId, int audience)
    {
        PlayId = playId;
        Audience = audience;
    }
}
