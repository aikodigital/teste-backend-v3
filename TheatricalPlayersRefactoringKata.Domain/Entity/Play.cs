using TheatricalPlayersRefactoringKata.Domain.Enum;

namespace TheatricalPlayersRefactoringKata.Domain.Entity;

public abstract class Play
{
    public string Name { get; private set; }
    public int Lines { get; private set; }
    public EnumGenres Type { get; protected set; }

    public Play(string name, int lines) {
        Name = name;
        Lines = Math.Clamp(lines, 1000, 4000);
    }
    
    public abstract decimal CalculateAmount(int audience);
    public abstract decimal CalculateCredits(int audience);
}
