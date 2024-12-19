using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Models
{
    public class Performance
    {
        [Key]
        [XmlElement] 
        public string PlayId { get; set; }

        [XmlElement] 
        public int Audience { get; set; }

        public Performance() { }

        public Performance(string playId, int audience)
        {
            PlayId = playId;
            Audience = audience;
        }
    }
}
