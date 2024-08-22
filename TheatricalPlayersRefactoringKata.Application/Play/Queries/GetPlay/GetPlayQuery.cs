namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetPlay;

public class GetPlayQuery
{
    public Guid Id { get; private set; }

    public GetPlayQuery(Guid id)
    {
        Id = id;
    }
}