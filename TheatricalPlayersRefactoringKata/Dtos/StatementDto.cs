using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Dtos
{
    [Serializable]
    [XmlRoot("Statement")]
    public class StatementDto
    {
        [XmlElement]
        public string Customer { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<StatementItemDto> Items { get; set; } = new List<StatementItemDto>();

        [XmlElement]
        public decimal AmountOwed { get; set; }

        [XmlElement]
        public int EarnedCredits { get; set; }
    }
}
