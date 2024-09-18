using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Models.Xml
{
    public class ItemsXml
    {
        [XmlElement("Item")]
        public List<Item> ItemList { get; set; }

        public ItemsXml()
        {
            ItemList = new List<Item>();
        }
    }
}
