namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Invoice
    {
        public string Id { get; set; }
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
