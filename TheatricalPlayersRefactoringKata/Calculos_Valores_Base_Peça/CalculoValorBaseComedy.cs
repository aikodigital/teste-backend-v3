using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Consts_Enum;

namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseComedy : ICalculoValoresBasePeça
{
    private readonly int valorAcrescentadoAposNumeroBaseEspectadores = 10000;
    private readonly int valorAdicionadoPorEspectador = 500;
    private readonly int numeroDePessoasBaseNaPlateia = 20;

    public int CalculaValoresBase(Invoice invoice, Play play)
    {
        var lines = play.Lines;

        if (lines < Constantes.minLines)
            lines = Constantes.minLines;

        if (lines > Constantes.maxLines)
            lines = Constantes.maxLines;

        var result = lines * Constantes.valorBaseCobrançaPeça;

        foreach (var perf in invoice.Performances)
        {
            if (perf.Audience > numeroDePessoasBaseNaPlateia)
            {
                result += valorAcrescentadoAposNumeroBaseEspectadores + valorAdicionadoPorEspectador * (perf.Audience - numeroDePessoasBaseNaPlateia);
            }
        }

        return result;
    }
}
