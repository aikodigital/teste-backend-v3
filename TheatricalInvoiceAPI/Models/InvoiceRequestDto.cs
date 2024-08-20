public class InvoiceRequestDto
{
    public string Customer { get; set; }
    public List<PerformanceDto> Performances { get; set; }
    public Dictionary<string, PlayDto> Plays { get; set; }
    public string Format { get; set; }

    public InvoiceRequestDto(string customer, List<PerformanceDto> performances, Dictionary<string, PlayDto> plays, string format)
    {
        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Performances = performances ?? throw new ArgumentNullException(nameof(performances));
        Plays = plays ?? throw new ArgumentNullException(nameof(plays));
        Format = format ?? throw new ArgumentNullException(nameof(format));
    }
}
