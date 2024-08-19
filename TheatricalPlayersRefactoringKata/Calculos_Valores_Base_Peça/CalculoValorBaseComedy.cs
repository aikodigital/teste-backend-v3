using TheatricalPlayersRefactoringKata.Consts_Enum;

namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseComedy : ICalculoValoresBasePeça
{
    private readonly decimal valorAcrescentadoAposNumeroBaseEspectadores = 100.00m;
    private readonly decimal valorAdicionadoPorEspectadorAcimaDoNumeroDePessoasBase = 5.00m;
    private readonly decimal valorBaseCalculadoPorEspectador = 3.00m;
    private readonly int numeroDePessoasBaseNaPlateia = 20;

    public decimal CalculaValoresBase(Performance perf, Play play)
    {
        var lines = play.Lines;

        if (lines < Constantes.minLines)
            lines = Constantes.minLines;

        if (lines > Constantes.maxLines)
            lines = Constantes.maxLines;

        decimal result = (decimal)lines / Constantes.valorASerDivididoPelasLinhas;

        if (perf.Audience > numeroDePessoasBaseNaPlateia)
        {
            result += valorAcrescentadoAposNumeroBaseEspectadores + valorAdicionadoPorEspectadorAcimaDoNumeroDePessoasBase * 
                        (perf.Audience - numeroDePessoasBaseNaPlateia);
        }

        result += valorBaseCalculadoPorEspectador * perf.Audience;

        return result;
    }
}
