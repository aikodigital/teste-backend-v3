using TheatricalPlayersRefactoringKata.Calculos_Valores_Base_Peça;

namespace TheatricalPlayersRefactoringKata.Factory;

public interface IPlayTypeFactory
{
    ICalculoValoresBasePeça FabricaDeTypes(Play play);
}
