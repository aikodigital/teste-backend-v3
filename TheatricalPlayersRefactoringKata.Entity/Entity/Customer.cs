namespace TheatricalPlayersRefactoringKata.Domain.Model.Entity
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Invoice> Invoices { get; set; }
    }
}