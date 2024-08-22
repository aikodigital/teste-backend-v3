using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public List<Performance> Performances { get; set; } = new List<Performance>();
    }
}
