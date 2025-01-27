using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.DTO
{
    [XmlRoot(ElementName = "Statement")]
    public class StatementDTO
    {
        public string Customer { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<ItemDTO> Items { get; set; }
        public double AmountOwed { get; set; }
        public double EarnedCredits { get; set; }
    }
}
