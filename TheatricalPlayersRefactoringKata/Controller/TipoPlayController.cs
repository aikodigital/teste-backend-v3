using System;
using TheatricalPlayersRefactoringKata;
using TheatricalPlayersRefactoringKata.CalculoBase;
using TheatricalPlayersRefactoringKata.Factory;

public class TipoPlayController : IFazerTipos
{
    private readonly IValores _comediaController;
    private readonly IValores _historicoController;
    private readonly IValores _tragediaController;

    public TipoPlayController(
        IValores comediaController,
        IValores historicoController,
        IValores tragediaController){
        _comediaController = comediaController ?? throw new ArgumentNullException(nameof(comediaController));
        _historicoController = historicoController ?? throw new ArgumentNullException(nameof(historicoController));
        _tragediaController = tragediaController ?? throw new ArgumentNullException(nameof(tragediaController));
    }

    public IValores FabricaDeTypes(PlayModel play)
    {
        return play.Type switch
        {
            "tragedy" => _tragediaController,
            "comedy" => _comediaController,
            "history" => _historicoController,
            _ => throw new Exception("unknown type: " + play.Type)
        };
    }
}