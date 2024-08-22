namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetPlay;

public class GetPlayQuery
{
    public string Name { get; private set; }

    public GetPlayQuery(string name)
    {
        Name = name;
    }
}