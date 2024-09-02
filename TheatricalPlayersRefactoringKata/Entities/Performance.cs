using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Performance
{
    public int Id { get; set; }
    [Required]
    public string PlayId { get; set; }
    public Play Play { get; set; }
    [Required]
    public int Audience { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }

    public Performance() { }

    public Performance(string playID, int audience)
    {
        PlayId = playID;
        Audience = audience;
    }

}
