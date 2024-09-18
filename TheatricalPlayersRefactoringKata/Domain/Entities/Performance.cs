using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    private string _playId;
    private int _audience;

    [Key]
    public int Id { get; set; }
    public string PlayId { get => _playId; set => _playId = value; }
    public int Audience { get => _audience; set => _audience = value; }
    public Play Play { get; set; }

    public Performance(string playID, int audience)
    {
        _playId = playID;
        _audience = audience;
    }
    public Performance() { }
}
