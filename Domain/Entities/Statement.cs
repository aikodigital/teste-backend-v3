using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Domain.Entities
{
    [Table("Statement")]
    public class Statement : BaseEntity
    {
        public string Customer { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
    }

    [Table("Item")]
    public class Item : BaseEntity
    {
        public string PlayName { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
        
        [XmlIgnore]
        public int StatementId { get; set; }
        [XmlIgnore]
        public Statement Statement { get; set; }

        
    }
}
