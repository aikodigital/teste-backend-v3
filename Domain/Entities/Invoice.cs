namespace Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Invoice(string customer, List<Performance> performances)
        {
            Customer = customer;
            Performances = performances ?? new List<Performance>();
        }
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
