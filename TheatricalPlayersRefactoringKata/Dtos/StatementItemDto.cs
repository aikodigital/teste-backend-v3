using System;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Dtos
{
    [Serializable]
    public class StatementItemDto
    {
        [XmlIgnore]
        public string Name { get; set; }

        [XmlElement]
        public decimal AmountOwed { get; set; }

        [XmlElement]
        public int EarnedCredits { get; set; }

        [XmlElement]
        public int Seats { get; set; }
    }
}
