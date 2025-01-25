using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string Customer {  get; set; }
        public List<Performance> Performances {  get; set; }
    }
}
