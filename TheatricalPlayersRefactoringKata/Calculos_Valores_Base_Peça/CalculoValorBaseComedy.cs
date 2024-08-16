using TheatricalPlayersRefactoringKata.Consts_Enum;

namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseComedy : ICalculoValoresBasePeça
{
    private readonly int valorAcrescentadoAposNumeroBaseEspectadores = 100;
    private readonly int valorAdicionadoPorEspectadorAcimaDoNumeroDePessoasBase = 5;
    private readonly int valorBaseCalculadoPorEspectador = 3;
    private readonly int numeroDePessoasBaseNaPlateia = 20;

    public int CalculaValoresBase(Performance perf, Play play)
    {
        var lines = play.Lines;

        if (lines < Constantes.minLines)
            lines = Constantes.minLines;

        if (lines > Constantes.maxLines)
            lines = Constantes.maxLines;

        var result = lines / Constantes.valorASerDivididoPelasLinhas;

        if (perf.Audience > numeroDePessoasBaseNaPlateia)
        {
            result += valorAcrescentadoAposNumeroBaseEspectadores + valorAdicionadoPorEspectadorAcimaDoNumeroDePessoasBase * 
                        (perf.Audience - numeroDePessoasBaseNaPlateia);
        }

        result += valorBaseCalculadoPorEspectador * perf.Audience;

        return result;
    }
}
