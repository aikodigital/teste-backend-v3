using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Application.DTOs;

public class PlayDTO
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public EnumGenres Type { get; private set; }

    public PlayDTO(string name, int lines, EnumGenres type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }
}