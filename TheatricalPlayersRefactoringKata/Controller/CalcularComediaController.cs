using TheatricalPlayersRefactoringKata.Enum;

namespace TheatricalPlayersRefactoringKata.Controller;

public class CalcularComediaController : IValores
{
    private const int ValorAcrescentadoAposNumeroBaseEspectadores = 100;
    private const int ValorAdicionadoPorEspectadorAcimaDoNumeroDePessoasBase = 5;
    private const int ValorBaseCalculadoPorEspectador = 3;
    private const int NumeroDePessoasBaseNaPlateia = 20;

    public decimal CalculoBase(PerformanceModel perf, PlayModel play)
    {
        decimal lines = play.Lines;

        if (lines < Constantes.minLines)
            lines = Constantes.minLines;

        if (lines > Constantes.maxLines)
            lines = Constantes.maxLines;

        var result = lines / Constantes.valorASerDivididoPelasLinhas;

        if (perf.Audience > NumeroDePessoasBaseNaPlateia)
        {
            result += ValorAcrescentadoAposNumeroBaseEspectadores + ValorAdicionadoPorEspectadorAcimaDoNumeroDePessoasBase * 
                        (perf.Audience - NumeroDePessoasBaseNaPlateia);
        }

        result += ValorBaseCalculadoPorEspectador * perf.Audience;

        return result;
    }
}
