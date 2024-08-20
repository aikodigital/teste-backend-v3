using TheatricalPlayersRefactoringKata;

public class InvoiceRequest
{
    public string Customer { get; set; }
    public List<PerformanceDto> Performances { get; set; }
    public List<PlayDto> Plays { get; set; }
    public string Format { get; set; }

  
  
    public InvoiceRequest(string customer, List<PerformanceDto> performances, Dictionary<string, Play> plays, string format)
    {
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Performances = performances ?? throw new ArgumentNullException(nameof(performances));
        Plays = plays.Values.Select(p => new PlayDto(p.Name, p.Type, p.Lines)).ToList();
        Format = format ?? throw new ArgumentNullException(nameof(format));
    }
}