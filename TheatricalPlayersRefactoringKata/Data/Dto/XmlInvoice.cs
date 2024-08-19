using System.Collections.Generic;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Data.Dto;

namespace TheatricalPlayersRefactoringKata.Data.Dto
{
    [XmlRoot("invoice")]
    public class XmlInvoice
    {
        [XmlElement("customer")]
        public string Customer { get; set; }

        [XmlElement("totalAmount")]
        public decimal TotalAmount { get; set; }

        [XmlElement("totalCredits")]
        public int TotalCredits { get; set; } 

        [XmlArray("performances")]
        [XmlArrayItem("performance")]
        public List<XmlPerformance> Performances { get; set; }

        public XmlInvoice()
        {
            Performances = new List<XmlPerformance>();
        }
    }
}