using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public interface ICalculoValoresBasePeça
{
    int CalculaValoresBase(Invoice invoice, Play play);
}
