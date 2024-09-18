using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Data
{
    [XmlRoot("Statement")]
    public class StatementData
    {
        public string Customer { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<ItemData> Items { get; set; } = new List<ItemData>();

        [XmlElement("AmountOwed")]
        public decimal TotalAmountOwed { get; set; }

        [XmlElement("EarnedCredits")]
        public int TotalEarnedCredits { get; set; }
    }
}
