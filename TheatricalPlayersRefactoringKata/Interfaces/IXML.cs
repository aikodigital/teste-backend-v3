using System.Collections.Generic;
using System.Xml;

namespace TheatricalPlayersRefactoringKata.XML;

public interface IXML
{
    XmlDocument SerializandoDados(InvoiceModel invoice, Dictionary<string, PlayModel> plays);
}