namespace TheatricalPlayersRefactoringKata;

public class Type
{
    private string _name;

    public string Name { get => _name; set => _name = value; }

    public Type(string name) {
        this._name = name;
    }
}