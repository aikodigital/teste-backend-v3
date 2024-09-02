namespace Domain.Entities;

public class Performance
{
    public int Id { get; set; }
    private string _playId;
    private int _audience;
    public decimal Value { get; set; }
    public decimal Credits { get; set; }

    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }

}
