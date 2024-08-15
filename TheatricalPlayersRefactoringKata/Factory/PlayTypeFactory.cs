using System;
using TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

namespace TheatricalPlayersRefactoringKata.Factory;

public class PlayTypeFactory : IPlayTypeFactory
{
    public ICalculoValoresBasePeça FabricaDeTypes(Play play)
    {
        ICalculoValoresBasePeça calculoValoresBasePeça;

        switch (play.Type)
        {
            case "tragedy":
                calculoValoresBasePeça = new CalculoValorBaseTragedy();
                break;
            case "comedy":
                calculoValoresBasePeça = new CalculoValorBaseComedy();
                break;
            default:
                throw new Exception("unknown type: " + play.Type);
        }

        return calculoValoresBasePeça;
    }
}
