using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.XML;

public interface ISerializaçaoDados
{
    Task SerializandoDados(Invoice invoice, Dictionary<string, Play> plays);
}
