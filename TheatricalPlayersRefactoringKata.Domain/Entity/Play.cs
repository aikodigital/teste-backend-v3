using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Entity;

public abstract class Play
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public EnumGenres Type { get; private set; }

    public Play(string name, int lines, EnumGenres type) {
        Name = name;
        Lines = Math.Clamp(lines, 1000, 4000);
        Type = type;
    }
    
    public abstract decimal CalculateAmount(int audience);
    public abstract decimal CalculateCredits(int audience);
}
