namespace TheatricalPlayersRefactoringKata.Communication.Responses;
public class ResponseShortExpense
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}