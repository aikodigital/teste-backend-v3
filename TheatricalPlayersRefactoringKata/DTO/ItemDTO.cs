using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Domain.DTO
{
    public class ItemDTO
    {
        [XmlIgnore]
        public string ItemName { get; set; }
        public double AmountOwed { get; set; }
        public double EarnedCredits { get; set; }
        public int Seats { get; set; }
    }
}
