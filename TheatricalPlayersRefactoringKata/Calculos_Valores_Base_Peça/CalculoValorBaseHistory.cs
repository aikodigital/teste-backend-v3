namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseHistory : ICalculoValoresBasePeça
{
    private readonly CalculoValorBaseComedy _calculoValorBaseComedy;
    private readonly CalculoValorBaseHistory _calculoValorBaseHistory;

    public int CalculaValoresBase(Performance perf, Play play)
    {
        var result = _calculoValorBaseComedy.CalculaValoresBase(perf, play);

        result += _calculoValorBaseHistory.CalculaValoresBase(perf, play);

        return result;
    }
}
