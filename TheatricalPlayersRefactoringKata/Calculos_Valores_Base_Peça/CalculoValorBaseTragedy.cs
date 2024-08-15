using TheatricalPlayersRefactoringKata.Consts_Enum;

namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseTragedy : ICalculoValoresBasePeça
{
    private readonly int valorAcrescentadoAposNumeroBaseEspectadores = 1000;
    private readonly int numeroDePessoasBaseNaPlateia = 30;

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
                result += valorAcrescentadoAposNumeroBaseEspectadores * (perf.Audience - numeroDePessoasBaseNaPlateia);
            }
        }

        return result;
    }
}
