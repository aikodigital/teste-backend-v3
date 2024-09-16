namespace TheatricalPlayers.Core.DataTransferObjects.StatementDTOs
{
    public class Invoice
    {
        public string Customer { get; set; }
        public IReadOnlyList<Performance> Performances { get; set; }
    }
}