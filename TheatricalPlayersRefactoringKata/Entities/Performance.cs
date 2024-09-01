using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Performance
{
    [Key]
    public int Id { get; set; }

    [Required, ForeignKey(nameof(PlayId))]
    public string PlayId;
    public Play Play { get; set; }    

    public int Audience;

    [Required, ForeignKey(nameof(InvoiceId))]
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }

    public Performance()
    {
        
    }

    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }

}
