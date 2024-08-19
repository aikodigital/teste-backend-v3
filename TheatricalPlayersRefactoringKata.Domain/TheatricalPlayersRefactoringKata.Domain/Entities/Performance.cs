using System.Text.Json.Serialization;
namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    public Performance()
    {

    }
    private long _id;
    private string _playId;
    private int _audience;

    public long Id { get => _id; set => _id = value; }
    public string PlayId { get => _playId; set => _playId = value; }

    public int Audience { get => _audience; set => _audience = value; }
    public Performance(long id,string playId, int audience)
    {
        _id = id;
        _audience = audience;
        _playId = playId;

    }

}
