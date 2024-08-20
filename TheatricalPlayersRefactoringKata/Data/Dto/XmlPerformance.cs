using System;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Data.Dto
{
    public class XmlPerformance
    {
        [XmlElement("playId")]
        public Guid PlayId { get; set; }

        [XmlElement("audience")]
        public int Audience { get; set; }

        [XmlElement("amount")]
        public decimal Amount { get; set; }

        [XmlElement("credits")]
        public int Credits { get; set; }

        [XmlElement("genre")]
        public string Genre { get; set; }
    }
}