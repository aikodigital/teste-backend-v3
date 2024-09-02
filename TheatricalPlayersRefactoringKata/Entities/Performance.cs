using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Performance
{
    [Key]
    public int Id { get; set; }

    [Required, ForeignKey(nameof(PlayId))]
    public string PlayId { get; set; }
    public virtual Play Play { get; set; }    

    public int Audience { get; set; }

    [Required, ForeignKey(nameof(InvoiceId))]
    public int InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; }

    public Performance()
    {
        
    }

    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }

}
