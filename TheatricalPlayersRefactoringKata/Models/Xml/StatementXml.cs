using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Models.Xml
{
    [XmlRoot("Statement")]
    public class Statement
    {
        public string Customer { get; set; }

        [XmlElement("Items")]
        public ItemsXml Items { get; set; }

        public string AmountOwed { get; set; }
        public string EarnedCredits { get; set; }
    }
}
