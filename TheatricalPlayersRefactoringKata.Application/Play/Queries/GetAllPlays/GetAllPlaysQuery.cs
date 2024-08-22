namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetAllPlay;

public class GetAllPlayQuery
{
    public string Name { get; private set; }

    public GetAllPlayQuery(string name)
    {
        Name = name;
    }
}