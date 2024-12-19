using TheatricalPlayersRefactoringKata.Models;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.API.DTOs
{
    public class TheaterInvoiceResponseDTO
    {
        public int InvoiceId { get; set; }
        public string Customer { get; set; }
        public string StatementXml { get; set; }
        public DateTime ProcessedAt { get; set; }
        [XmlArray] 
        [XmlArrayItem("Performance")] 
        public List<Performance> Performances { get; set; } = new List<Performance>(); 
    }
}
