using System;
using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata.Models;

public class Play
{
    public string Name { get; set; }
    public int Lines { get; set; }
    public EPlayType Type { get; set; }

    public Play(string name, int lines, EPlayType type)
    {
        Name = name.Length >= 3 ? name : throw new ArgumentException("Name must be greater than 3 characters");
        Lines = lines > 0 ? lines : throw new ArgumentException("Lines must be greater than 0");
        Type = type == EPlayType.Tragedy || type == EPlayType.Comedy || type == EPlayType.History ? type : throw new ArgumentException("Play Type must be Tragedy, Comedy or History");
    }
}
