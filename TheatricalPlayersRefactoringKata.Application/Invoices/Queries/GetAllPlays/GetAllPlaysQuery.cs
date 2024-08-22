namespace TheatricalPlayersRefactoringKata.Application.Play.Queries.GetAllInvoices;

public class GetAllInvoicesQuery
{
    public string Name { get; private set; }

    public GetAllInvoicesQuery(string name)
    {
        Name = name;
    }
}