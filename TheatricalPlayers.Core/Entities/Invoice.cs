namespace TheatricalPlayers.Core.Entities
{
    public class Invoice
    {
        public string Customer { get; set; }
        public IReadOnlyList<Performance> Performances { get; set; }
    }
}