using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Models;

public class Performance
{
    private int _audience;
    private Play _play;

    [Key]
    public int Id {  get; set; }
    [ForeignKey("InvoiceId")]
    [InverseProperty("Invoice")]
    public int InvoiceId {  get; set; }
    public Invoice Invoice { get; set; }

    [ForeignKey("PlayId")]
    [InverseProperty("Play")]
    public int PlayId { get; set; }

    public int Audience { get => _audience; set => _audience = value; }
    public Play Play { get => _play; set => _play = value; }

    public Performance(int audience, Play play)
    {

        _audience = audience;
        _play = play;

    }

}
