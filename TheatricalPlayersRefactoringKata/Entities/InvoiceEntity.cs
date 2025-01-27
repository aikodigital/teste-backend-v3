using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    [Table("Invoice")]
    public class InvoiceEntity
    {
        [Key]
        public int Id { get; set; }
        public string Customer {  get; set; }
        public List<PerformanceEntity> Performances {  get; set; }
    }
}
