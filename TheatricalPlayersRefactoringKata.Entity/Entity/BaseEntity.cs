namespace TheatricalPlayersRefactoringKata.Domain.Model.Entity
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}