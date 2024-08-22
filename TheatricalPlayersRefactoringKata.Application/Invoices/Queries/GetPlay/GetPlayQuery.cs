namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetInvoices;

public class GetInvoicesQuery
{
    public string Name { get; private set; }

    public GetInvoicesQuery(string name)
    {
        Name = name;
    }
}