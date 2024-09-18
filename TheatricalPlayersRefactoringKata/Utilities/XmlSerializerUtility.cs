using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Utilities
{
    public static class XmlSerializerUtility
    {
        public static XmlSerializer CreateSerializer<T>()
        {
            return new XmlSerializer(typeof(T));
        }
    }
}
