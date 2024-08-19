namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public Guid PerformanceId{ get; set; }
        public List<Performance> Performances { get; set; }

        public Invoice()
        {
        }

        public Invoice(Guid id, string customer, List<Performance> performances)
        {
            Id = id;
            Customer = customer;
            Performances = performances;
        }
    }
}