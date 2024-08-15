using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Performance
{
    private string _playId;
    private int _audience;

     public PlayTypes PlayId { get; set; }
    public int Audience { get => _audience; set => _audience = value; }

    public Performance(int audience)
    {
        _audience = audience;
        _playId = PlayId.ToString();

    }

}
