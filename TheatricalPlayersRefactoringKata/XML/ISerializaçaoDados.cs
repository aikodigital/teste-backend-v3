using System.Collections.Generic;
using System.Xml;

namespace TheatricalPlayersRefactoringKata.XML;

public interface ISerializaçaoDados
{
    XmlDocument SerializandoDados(Invoice invoice, Dictionary<string, Play> plays);
}
