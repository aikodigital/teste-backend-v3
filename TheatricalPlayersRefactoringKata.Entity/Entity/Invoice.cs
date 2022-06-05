namespace TheatricalPlayersRefactoringKata.Domain.Model.Entity
{
    public class Invoice : BaseEntity
    {
        public long CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalCredits { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<Performance> Performances { get; set; }
    }
}