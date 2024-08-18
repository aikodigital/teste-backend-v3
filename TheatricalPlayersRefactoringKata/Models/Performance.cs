using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    public int Id { get; set; }
    public string PlayName { get; set; }  // Referência ao Name do Play
    public int Audience { get; set; }
    public int? InvoiceId { get; set; }
    public virtual Play Play { get; set; }
    public virtual Invoice Invoice { get; set; }

    public Performance()
    {
    }

    public Performance(string playName, int audience)
    {
        PlayName = playName;
        Audience = audience;
    }
}
