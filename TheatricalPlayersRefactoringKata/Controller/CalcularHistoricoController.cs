using System;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.CalculoBase;
using TheatricalPlayersRefactoringKata.Controller;

public class CalcularHistoricoController : IValores
{
    private readonly CalcularComediaController _comediaController;
    private readonly CalcularTragediaController _tragediaController;

    public CalcularHistoricoController(CalcularComediaController comediaController, CalcularTragediaController tragediaController)
    {
        _comediaController = comediaController ?? throw new ArgumentNullException(nameof(comediaController));
        _tragediaController = tragediaController ?? throw new ArgumentNullException(nameof(tragediaController));
    }

    public decimal CalculoBase(PerformanceModel perf, PlayModel play)
    {
        var result = _tragediaController.CalculoBase(perf, play);
        result += _comediaController.CalculoBase(perf, play);
        return result;
    }
}
