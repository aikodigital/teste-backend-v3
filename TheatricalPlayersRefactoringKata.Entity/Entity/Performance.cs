namespace TheatricalPlayersRefactoringKata.Domain.Model.Entity
{
    public class Performance : BaseEntity
    {
        public long PlayId { get; set; }
        public long InvoiceId { get; set; }
        public int Audience { get; set; }

        public virtual Play Play { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}