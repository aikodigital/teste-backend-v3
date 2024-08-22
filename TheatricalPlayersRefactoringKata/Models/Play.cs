using System;
using TheatricalPlayersRefactoringKata.Enums;

namespace TheatricalPlayersRefactoringKata.Models;

public class Play
{
    private string _name;
    private int _lines;
    private EPlayType _type;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public EPlayType Type { get => _type; set => _type = value; }

    public Play(string name, int lines, EPlayType type)
    {
        _name = name.Length >= 3 ? name : throw new ArgumentException("Name should be greater than 3 characters");
        _lines = lines > 0 ? lines : throw new ArgumentException("Lines should be greater than 0");
        _type = type == EPlayType.Tragedy || type == EPlayType.Comedy || type == EPlayType.History ? type : throw new ArgumentException("Type should be Tragedy, Comedy or History");
    }
}
