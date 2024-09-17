using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata
{
    [XmlType("Item")]
    public class ItemData
    {
        [XmlIgnore]
        public string PlayName { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
    }
}
