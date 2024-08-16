using System.Text.Json.Serialization;
namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    private readonly long _id;
    private string _playId;
    private int _audience;

    public string PlayId { get => _playId; set => _playId = value; }

    public int Audience { get => _audience; set => _audience = value; }
    [JsonConstructor]
    public Performance(long id,string playId, int audience)
    {
        _id = id;
        _audience = audience;
        _playId = playId;

    }

}
