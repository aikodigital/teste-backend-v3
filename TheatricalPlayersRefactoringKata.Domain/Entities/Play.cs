using System.Reflection;

namespace TheatricalPlayersRefactoringKata.Domain;

public class Play
{
    private string _name;
    private int _lines;
    private string _gender;

    public string Name { get => _name; set => _name = value; }
    public int Lines { get => _lines; set => _lines = value; }
    public string Gender { get => _gender; set => _gender = value; }

    public Play(string name, int lines, string gender) {
        this._name = name;
        this._lines = lines;
        this._gender = gender;
    }
}
