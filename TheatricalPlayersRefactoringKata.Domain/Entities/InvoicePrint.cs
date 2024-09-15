using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.Entities
{
    public class InvoicePrint
    {
        [XmlRoot(ElementName = "Item")]
        public class Item
        {
            [XmlIgnore]
            public string Name { get; set; } = "";

            [XmlIgnore]
            public decimal AmountOwed { get; set; } = 0;
            [XmlElement(ElementName = "AmountOwed")]
            public string AmountOwedFormatted
            {
                get
                {
                    if (AmountOwed % 1 == 0) return AmountOwed.ToString("n0").Replace(".", "").Replace(",", ".");
                    else return AmountOwed.ToString("n1").Replace(".", "").Replace(",", ".");
                }
                set
                {
                    AmountOwed = decimal.Parse(value);
                }
            }

            [XmlElement(ElementName = "EarnedCredits")]
            public decimal EarnedCredits { get; set; } = 0;

            [XmlElement(ElementName = "Seats")]
            public int Seats { get; set; } = 0;
        }

        [XmlRoot(ElementName = "Items")]
        public class Items
        {

            [XmlElement(ElementName = "Item")]
            public List<Item> Item { get; set; } = new List<Item>();
        }

        [XmlRoot(ElementName = "Statement")]
        public class Statement
        {

            [XmlElement(ElementName = "Customer")]
            public string Customer { get; set; } = "";

            [XmlElement(ElementName = "Items")]
            public Items Items { get; set; } = new Items();

            [XmlIgnore]
            public decimal AmountOwed { get; set; } = 0;
            [XmlElement(ElementName = "AmountOwed")]
            public string AmountOwedFormatted
            {
                get => AmountOwed.ToString("n1").Replace(".", "").Replace(",", ".");
                set => AmountOwed = decimal.Parse(value);
            }

            [XmlElement(ElementName = "EarnedCredits")]
            public decimal EarnedCredits { get; set; } = 0;
        }
    }
}
