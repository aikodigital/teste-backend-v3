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

        [XmlElement("genre")]
        public string Genre { get; set; }
    }
}