using TheatricalPlayersRefactoringKata.Consts_Enum;

namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseTragedy : ICalculoValoresBasePeça
{
    private readonly decimal valorAcrescentadoAposNumeroBaseEspectadores = 10.00m;
    private readonly int numeroDePessoasBaseNaPlateia = 30;

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
            result += valorAcrescentadoAposNumeroBaseEspectadores * (perf.Audience - numeroDePessoasBaseNaPlateia);
        }

        return result;
    }
}
