
namespace Main.Contracts.StatementPrinter
{
    public record StatementPrinterRequest(Invoice invoice, Dictionary<string,Play> plays);

    public class Invoice
    {
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; }
    }

    public class Performance
    {
        public string PlayId { get; set; }

        public int Audience { get; set; }
    }

    public class Play
    {
        public string Name { get; set; }
        public int Lines { get; set; }
        public string Type { get; set; }
    }
}
