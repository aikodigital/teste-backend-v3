using TheatricalPlayersRefactoringKata.Model;
using TheatricalPlayersRefactoringKata.Service.Calculators.Interface;

namespace TheatricalPlayersRefactoringKata.Service.Calculators;

public class StatementCalculator(IGenreCalculator genreCalculator)
{
    private const uint minLines = 1000;
    private const uint maxLines = 4000;

    public uint VolumeCredits { get; private set; }
    public uint ThisAmount { get; private set; }

    private IGenreCalculator _genreCalculator = genreCalculator;

    public uint CalculateAmountOwned(Performance perf, Play play)
    {
        var lines = play.Lines;
        if (lines < minLines) lines = minLines;
        if (lines > maxLines) lines = maxLines;

        var amount = _genreCalculator.CalculateGenre((uint)perf.Audience, (uint)lines);

        return amount;
    }
    
    public uint CalculateCredits(uint audience)
    {
        return _genreCalculator.CalculateCredits(audience);
    }
}
