using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.CalculoBase;

public class CalcularTragediaController : IValores
{
    private readonly int valorAcrescentadoAposNumeroBaseEspectadores = 10;
    private readonly int numeroDePessoasBaseNaPlateia = 30;

    public decimal CalculoBase(PerformanceModel perf, PlayModel play)
    {
        decimal lines = play.Lines;

        if (lines < Constantes.minLines)
            lines = Constantes.minLines;

        if (lines > Constantes.maxLines)
            lines = Constantes.maxLines;

        var result = lines / Constantes.valorASerDivididoPelasLinhas;

        if (perf.Audience > numeroDePessoasBaseNaPlateia)
        {
            result += valorAcrescentadoAposNumeroBaseEspectadores * (perf.Audience - numeroDePessoasBaseNaPlateia);
        }

        return result;
    }
}