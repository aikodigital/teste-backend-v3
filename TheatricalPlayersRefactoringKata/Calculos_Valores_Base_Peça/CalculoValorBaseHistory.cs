namespace TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

public class CalculoValorBaseHistory : ICalculoValoresBasePeça
{
    private readonly CalculoValorBaseComedy _calculoValorBaseComedy;
    private readonly CalculoValorBaseTragedy _calculoValorBaseTragedy;

    public CalculoValorBaseHistory()
    {
        _calculoValorBaseComedy = new CalculoValorBaseComedy();
        _calculoValorBaseTragedy = new CalculoValorBaseTragedy();
    }

    public decimal CalculaValoresBase(Performance perf, Play play)
    {
        decimal result = _calculoValorBaseComedy.CalculaValoresBase(perf, play);

        result += _calculoValorBaseTragedy.CalculaValoresBase(perf, play);

        return result;
    }
}
