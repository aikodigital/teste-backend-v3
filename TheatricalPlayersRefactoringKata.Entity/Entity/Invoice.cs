namespace TheatricalPlayersRefactoringKata.Domain.Model.Entity
{
    public class Invoice : BaseEntity
    {
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
        public decimal Credits { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual List<Performance> Performances { get; set; }
    }
}