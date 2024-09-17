using TheatricalPlayersRefactoringKata.Model;

namespace TheatricalPlayersRefactoringKata.Helper;
public class AjustaLinhasHelper
{
    private const int MinLines = 1000;
    private const int MaxLines = 4000;

    public static int AjustaLimiteMinMaxDeLinhas(Play play)
    {
        int lines = play.Lines;

        if (lines < MinLines)
        {
            lines = MinLines;
        }

        if (lines > MaxLines)
        {
            lines = MaxLines;
        }

        return lines;
    }
}
