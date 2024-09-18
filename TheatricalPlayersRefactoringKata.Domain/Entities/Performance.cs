using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Performance
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int _performanceId;
    private int _playId;
    private int _audience;
    private int _invoiceId;

    public int PerformanceId { get => _performanceId; set => _performanceId = value; }
    public int PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public int InvoiceId { get => _invoiceId; set => _invoiceId = value; }

    public Performance() { }
    public Performance(int performanceId, int playID, int audience, int invoiceId)
    {
        this._performanceId = performanceId;
        this._playId = playID;
        this._audience = audience;
        this.InvoiceId = invoiceId;
    }

}
