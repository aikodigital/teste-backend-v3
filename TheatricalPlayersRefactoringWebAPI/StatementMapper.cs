using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringWebAPI.DTO;

namespace TheatricalPlayersRefactoringWebAPI
{
    public class StatementMapper
    {
        public Invoice MapToInvoice(InvoiceRequest request)
        {
            return new Invoice(
                request.Customer,
                request.Performances.Select(p => new Performance(p.PlayId, p.Audience)).ToList()
            );
        }

        public Dictionary<string, Play> MapToPlays(List<PlayRequest> requests)
        {
            var plays = new Dictionary<string, Play>();
            foreach (var request in requests)
            {
                plays.Add(request.PlayId.ToLower(), new Play(request.Name, request.Lines, request.Type));
            }
            return plays;
        }
    }
}
