namespace TheatricalPlayersRefactoringKata.Data
{
    public class Performance
    {
        public int Id { get; set; }
        public int Audience { get; set; }
      
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int PlayId { get; set; }
        public Play Play { get; set; }
    }
}
