namespace TheatricalPlayersRefactoringKata.Entities;

public class Performance
{
    private int _id;
    private string _playId;
    private int _audience;
    private Invoice _invoice;
    private int _invoiceId;

    public int Id { get => _id; }
    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public Invoice Invoice { get => _invoice; }
    public int InvoiceId { get => _invoiceId; }

    // Entity Framework
    public Performance() { }
    public Performance(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }

}
